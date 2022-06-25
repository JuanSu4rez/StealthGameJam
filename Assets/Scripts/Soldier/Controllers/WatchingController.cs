﻿using UnityEngine;
using System.Linq;

public class WatchingController : MonoBehaviour, IWatchingHandler
{
    private SoldierMachineState _soldierMachineState;
    public LayerMask WallLayerMask;
    private CapsuleCollider _capsuleCollider;
    public bool IamAttacking = false;
    public float stamina = 1;
    // Use this for initialization
    void Start() {
        _soldierMachineState = this.GetComponent<SoldierMachineState>();
        var aux = this.GetComponents<Collider>().Select(p => p.GetType().FullName);
        ////Debug.Log(string.Join(";", aux.ToArray()));
        _capsuleCollider = this.GetComponent<CapsuleCollider>();

    }
    // Update is called once per frame
    void Update() {
        if(!_soldierMachineState.PlayerIsAlive) {
            return;
        }
        if(
            _soldierMachineState.ValidateState(SoldierStates.attacking) &&
           _soldierMachineState.AttackingState.AttackState == AttackingStatesValues.chasing
           ) {


            if(_soldierMachineState.LocomotionState.HasReachThePoint) {
                this.stamina -= 0.3f;
                if(stamina > 0.5) {
                    GoToAttack(_soldierMachineState.AttackingState.Player, stamina);
                }
                else {
                    this.stamina = 1;
                    _soldierMachineState.SetState(_soldierMachineState.PatrolState);
                }
            }
        }
    }

    public void HandleOnvisionExit(Collider other) {
        if(IamAttacking) {
            stamina = 1;
            GoToAttack(_soldierMachineState.AttackingState.Player, stamina);
        }
    }

    public void HandleWatching(Collider collider) {
        if(!_soldierMachineState.PlayerIsAlive) {
            return;
        }
        if(
            _soldierMachineState.ValidateState(SoldierStates.attacking) &&
            _soldierMachineState.AttackingState.AttackState == AttackingStatesValues.attacking
            ) {
            return;
        }
        if(PlayerIsBehindOfAWall()) {
            return;
        }

        StartAttack(collider.transform.gameObject);
    }



    public bool PlayerIsBehindOfAWall() {
        var controller = _soldierMachineState.AttackingState?.Player?.GetComponent<PlayerController>();
        var result = false;
        if(controller != null) {
            result =  controller.CollideWithObstacle(this.gameObject);
        }
        Debug.Log("PlayerIsBehindOfAWall " + result);
        return result;
    }

    public void StartAttack(GameObject gameObject) {
        IamAttacking = true;
        _soldierMachineState.AttackingState.Player = gameObject;
        _soldierMachineState.AttackingState.PointToGo = null;
        _soldierMachineState.AttackingState.AttackState = AttackingStatesValues.attacking;
        _soldierMachineState.AttackingState.StartAttack();
        _soldierMachineState.SetState(_soldierMachineState.AttackingState);
    }

    public void GoToAttack(GameObject gameObject, float stamina = 1) {
        if(!_soldierMachineState.PlayerIsAlive) {
            return;
        }

        float chasingVelocity = 4f * stamina;
        var distance = this.gameObject.transform.position - gameObject.transform.position;
        _soldierMachineState.AttackingState.Player = gameObject.transform.gameObject;
        var normalizedDistance = distance.normalized;
        var pointToGo = gameObject.transform.position;// + ( normalizedDistance * 2 );
       // Debug.DrawLine(pointToGo, pointToGo + Vector3.up * 10, Color.red, 20);
        _soldierMachineState.AttackingState.PointToGo = pointToGo;
        _soldierMachineState.AttackingState.AttackState = AttackingStatesValues.chasing;
        if(!_soldierMachineState.ValidateState(SoldierStates.attacking)) { 
            //SET NEW STATE
            _soldierMachineState.SetState(_soldierMachineState.AttackingState);
        }
        _soldierMachineState.LocomotionState.SetDestiny(pointToGo, chasingVelocity);
        IamAttacking = false;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingState : MonoBehaviour, ISoldierState
{
    public SoldierStates SoldierState { get => SoldierStates.attacking; }
    public AttackingStatesValues AttackState { get; set; } = AttackingStatesValues._none;
    public Vector3? PointToGo { get; set; }
    public bool IsOnPositionToAttack { get; set; }
    private IWeaponBehaviour weaponBehaviour = null;
    public float MinmunDistance = 5;
    [SerializeField]
    private GameObject weapon;
    public GameObject Player {
        get;
        set;
    }


    void Start() {
        this.GetComponent<IWeaponBehaviour>();
    }
    void Update() {
        if(PointToGo.HasValue)
            Debug.DrawLine(PointToGo.Value, PointToGo.Value + ( Vector3.up * 5 ), Color.red);

        switch(AttackState) {
            case AttackingStatesValues.chasing:

                break;
            case AttackingStatesValues.attacking:
                //sent ray to validate if there is a wall
                this.transform.LookAt(Player.transform.position);
                Debug.DrawLine(this.transform.position, Player.transform.position, Color.cyan);
                break;
        }

    }

    public void StartAttack() {
        if(AttackState == AttackingStatesValues.chasing) { 
            AttackState = AttackingStatesValues.attacking;

        }
        weaponBehaviour = weapon?.GetComponent<IWeaponBehaviour>();
        if(weaponBehaviour != null) {
            weaponBehaviour?.SetTarget(Player);
        }
    }

    public void OnDisable() {
        //this can be called from any of the StateMachineBehaviour 
        weaponBehaviour?.Disable();
    }

    public Vector3 DistanceWithThePlayer(){
        var distance = this.gameObject.transform.position - Player.transform.position;
        return distance;
    }

    public  bool ValidateDistance(Vector3 distance) {
        //Debug.Log("ValidateDistance "+ distance.magnitude);
        return distance.magnitude <= (MinmunDistance)  ;
    }

    public bool IsOnDistanceToAttack() {

        var controller = Player?.GetComponent<PlayerController>();
        if(controller != null) {
            return
              !controller.CollideWithObstacle(this.gameObject) && 
              ValidateDistance(
                DistanceWithThePlayer()
              );
        }
        return false;
    }

}

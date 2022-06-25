using UnityEngine;
using System.Linq;

public class WatchingController : MonoBehaviour, IWatchingHandler
{
    private SoldierMachineState _soldierMachineState;
    public LayerMask WallLayerMask;
    private CapsuleCollider _capsuleCollider;
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
        if(_soldierMachineState.ValidateState(SoldierStates.attacking)) {

            if(_soldierMachineState.AttackingState.AttackState == AttackingStatesValues.attacking) {
                if(PlayerIsBehindOfAWall()) {
                    _soldierMachineState.SetState(_soldierMachineState.PatrolState);
                }
                else {
                    var distance = this.gameObject.transform.position - _soldierMachineState.AttackingState.Player.transform.position;
                    if(distance.magnitude > _soldierMachineState.AttackingState.MinmunDistance ) {
                        GoToAttack(_soldierMachineState.AttackingState.Player);
                    } /**/
                }
            }
            else if(_soldierMachineState.AttackingState.AttackState == AttackingStatesValues.chasing) {

                /*
                if(!_soldierMachineState.LocomotionState.HasReachThePoint) {

                    var distance = this.gameObject.transform.position - _soldierMachineState.AttackingState.Player.transform.position;

                    var stumbleWithThePlayer = distance.magnitude <= _soldierMachineState.AttackingState.MinmunDistance;

                    if(stumbleWithThePlayer) {
                        StartAttack();
                    }

                }
                else {

                }*/
                if(_soldierMachineState.LocomotionState.HasReachThePoint) {
                    Debug.Log("Llegue al punto parido");
                    //searching
                    _soldierMachineState.SetState(_soldierMachineState.PatrolState);
                }
                
               
            }
        }

    }
    public void HandleWatching(Collider collider) {
        if(!_soldierMachineState.PlayerIsAlive) {
            return;
        }
        if(_soldierMachineState.AttackingState.AttackState == AttackingStatesValues.attacking) {
            return;
        }
        if(PlayerIsBehindOfAWall()) {
            return;
        }
        _soldierMachineState.AttackingState.Player = collider.transform.gameObject;
        StartAttack();
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

    public void StartAttack() {
        _soldierMachineState.AttackingState.PointToGo = null;
        _soldierMachineState.AttackingState.AttackState = AttackingStatesValues.attacking;
        _soldierMachineState.AttackingState.StartAttack();
        _soldierMachineState.SetState(_soldierMachineState.AttackingState);
    }

    public void GoToAttack(GameObject gameObject) {
        if(!_soldierMachineState.PlayerIsAlive) {
            return;
        }

        float chasingVelocity = 4;
        var distance = this.gameObject.transform.position - gameObject.transform.position;
        _soldierMachineState.AttackingState.Player = gameObject.transform.gameObject;
        var normalizedDistance = distance.normalized;
        var pointToGo = gameObject.transform.position + ( normalizedDistance * 2 );
        Debug.DrawLine(pointToGo, pointToGo + Vector3.up * 10, Color.red, 20);
        _soldierMachineState.AttackingState.PointToGo = pointToGo;
        _soldierMachineState.AttackingState.AttackState = AttackingStatesValues.chasing;
        _soldierMachineState.SetState(_soldierMachineState.AttackingState);
        _soldierMachineState.LocomotionState.SetDestiny(pointToGo, chasingVelocity);
    }
}

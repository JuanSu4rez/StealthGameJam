using UnityEngine;
using System.Linq;

public class WatchingController : MonoBehaviour, IWatchingHandler
{
    private SoldierMachineState _soldierMachineState;
    public LayerMask WallLayerMask;
    private CapsuleCollider _capsuleCollider;
    public bool IamAttacking = false;
    public float stamina = 1;
    public float staminaReduction = 0.3f;
    public GameObject Player {
        get;
        set;
    }
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
                this.stamina -= staminaReduction;
                if(stamina > 0.5) {
                    GoToAttack(_soldierMachineState.AttackingState.Player, stamina);
                }
                else {
                    this.stamina = 1;
                    Player = null;
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
        Player = collider.gameObject;
        if(
            _soldierMachineState.ValidateState(SoldierStates.attacking) &&
            _soldierMachineState.AttackingState.AttackState == AttackingStatesValues.attacking
            ) {
            return;
        }


        var isBehindOfaWall = PlayerIsBehindOfAWall();
        if(isBehindOfaWall) {
            if(IamAttacking) {
                GoToAttack(_soldierMachineState.AttackingState.Player, stamina);
            }
            return;
        }
        else { 
            StartAttack(collider.transform.gameObject);
        }
    }



    public bool PlayerIsBehindOfAWall() {
        var controller = Player.GetComponent<PlayerController>();
        var result = false;
        if(controller != null) {
            result = controller.CollideWithObstacle(this.gameObject);
        }
        Debug.Log("PlayerIsBehindOfAWall " + result);
        return result;
    }

    public void StartAttack(GameObject gameObject) {
        if(IamAttacking)
            return;

        IamAttacking = true;
        _soldierMachineState.AttackingState.Player = gameObject;
        _soldierMachineState.AttackingState.PointToGo = null;
        _soldierMachineState.AttackingState.AttackState = AttackingStatesValues.attacking;
        _soldierMachineState.AttackingState.StartAttack();
        if(!_soldierMachineState.LocomotionState.HasReachThePoint) {
            _soldierMachineState.LocomotionState.Stop();
        }
        if(!_soldierMachineState.ValidateState(SoldierStates.attacking)) {
            //SET NEW STATE
            _soldierMachineState.SetState(_soldierMachineState.AttackingState);
        }
    }

    public void GoToAttack(GameObject gameObject, float stamina = 1) {
        if(!_soldierMachineState.PlayerIsAlive) {
            return;
        }

        float chasingVelocity = 4f * stamina;
        var distance = this.gameObject.transform.position - gameObject.transform.position;
        _soldierMachineState.AttackingState.Player = gameObject.transform.gameObject;
        var normalizedDistance = distance.normalized;
        var pointToGo = gameObject.transform.position;
        _soldierMachineState.AttackingState.PointToGo = pointToGo;
        _soldierMachineState.AttackingState.StartChasing();

        if(!_soldierMachineState.ValidateState(SoldierStates.attacking)) {
            //SET NEW STATE
            _soldierMachineState.SetState(_soldierMachineState.AttackingState);
        }
        _soldierMachineState.LocomotionState.SetDestiny(pointToGo, chasingVelocity);
        IamAttacking = false;
    }
}

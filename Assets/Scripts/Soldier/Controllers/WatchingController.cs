using UnityEngine;
using System.Collections;
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
        //Debug.Log(string.Join(";", aux.ToArray()));
        _capsuleCollider = this.GetComponent<CapsuleCollider>();

    }
    // Update is called once per frame
    void Update() {
        if(!_soldierMachineState.PlayerIsAlive) {
            return;
        }
        if(_soldierMachineState.ValidateState(SoldierStates.attacking)) {

            if(_soldierMachineState.AttackingState.AttackState == AttackingStatesValues.attacking && PlayerIsBehingOfAWall() ) {
                //Debug.Log("PlayerIsBehingOfAWall true");
                _soldierMachineState.SetState(_soldierMachineState.PatrolState);
            }
        }

    }
    public void HandleWatching(Collider collider) {
        if(!_soldierMachineState.PlayerIsAlive) {
            return;
        }

        if(_soldierMachineState.ValidateState(SoldierStates.attacking)) {
            return;
        }
        //sent ray to validate if there is a wall
        if(PlayerIsBehingOfAWall()) {
            return;
        }

        _soldierMachineState.AttackingState.Player = collider.transform.gameObject;

        var distance = this.gameObject.transform.position - collider.transform.position;
        _soldierMachineState.AttackingState.Player = collider.transform.gameObject;
        if(distance.magnitude > _soldierMachineState.AttackingState.MinmunDistance) {
            var normalizedDistance = distance.normalized;
            var pointtogo = collider.transform.position + ( normalizedDistance * _soldierMachineState.AttackingState.MinmunDistance );
            _soldierMachineState.AttackingState.PointToGo = pointtogo;
            _soldierMachineState.AttackingState.AttackState = AttackingStatesValues.chasing;
            _soldierMachineState.SetState(_soldierMachineState.AttackingState);
        }
        else {
            _soldierMachineState.AttackingState.PointToGo = null;
            _soldierMachineState.AttackingState.AttackState = AttackingStatesValues.attacking;
            _soldierMachineState.SetState(_soldierMachineState.AttackingState);
        }
    }

    public bool PlayerIsBehingOfAWall() {
        var controller = _soldierMachineState.AttackingState?.Player?.GetComponent<PlayerController>();
        if(controller != null) {
            return controller.CollideWithObstacle(this.gameObject);
        }
        else {
            return false;
        }
    }

    public void GoToAttack(GameObject gameObject) {
        if(!_soldierMachineState.PlayerIsAlive) {
            return;
        }

        if(_soldierMachineState.ValidateState(SoldierStates.attacking)) {
            return;
        }

        var distance = this.gameObject.transform.position - gameObject.transform.position;
        _soldierMachineState.AttackingState.Player = gameObject.transform.gameObject;
        var normalizedDistance = distance.normalized;
        var pointtogo = gameObject.transform.position + ( normalizedDistance * 2 );
        Debug.DrawLine(pointtogo, pointtogo + Vector3.up * 10, Color.red, 20);
        _soldierMachineState.AttackingState.PointToGo = pointtogo;
        _soldierMachineState.AttackingState.AttackState = AttackingStatesValues.chasing;
        _soldierMachineState.SetState(_soldierMachineState.AttackingState);
    }
}

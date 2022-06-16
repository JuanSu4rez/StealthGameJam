using UnityEngine;
using System.Collections;
using System.Linq;

public class WatchingController : MonoBehaviour, IWatchingHandler
{
    private SoilderMachineState _soldierMachineState;
    public LayerMask WallLayerMask;
    private int minmunDistance = 5;
    private CapsuleCollider _capsuleCollider;
    // Use this for initialization
    void Start() {
        _soldierMachineState = this.GetComponent<SoilderMachineState>();
        var aux = this.GetComponents<Collider>().Select(p => p.GetType().FullName);
        Debug.Log(string.Join(";", aux.ToArray()));
        _capsuleCollider = this.GetComponent<CapsuleCollider>();

    }
    // Update is called once per frame
    void Update() {
        if(_soldierMachineState.ValidateState(SoldierStates.attacking)) {

            if(PlayerIsBehingOfAWall()) {
                Debug.Log("PlayerIsBehingOfAWall true");
                _soldierMachineState.SetState(_soldierMachineState.PatrolState);
            }
        }

    }
    public void HandleWatching(Collider collider) {
        if(_soldierMachineState.ValidateState(SoldierStates.attacking)) {
            return;
        }
        //sent ray to validate if there is a wall
        if(PlayerIsBehingOfAWall()) {
            return;
        }
        var distance = this.gameObject.transform.position - collider.transform.position;
        _soldierMachineState.AttackingState.Player = collider.transform.gameObject;
        if(distance.magnitude > minmunDistance) {
            var normalizedDistance = distance.normalized;
            var pointtogo = collider.transform.position + ( normalizedDistance * minmunDistance );
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
            return controller.CollideWithWall(this.gameObject);
        }
        else {
            return false;
        }
    }
}

using UnityEngine;
using System.Collections;

public class WatchingController : MonoBehaviour, IWatchingHandler
{
    private SoilderMachineState _soldierMachineState;
    private int minmunDistance = 5;
    // Use this for initialization
    void Start() {
        _soldierMachineState = this.GetComponent<SoilderMachineState>();
    }
    // Update is called once per frame
    
    public void HandleWatching(Collider collider) {
        if(_soldierMachineState.ValidateState(SoldierStates.attacking)) {
            return;
        }
        //sent ray to validate if there is a wall
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
}

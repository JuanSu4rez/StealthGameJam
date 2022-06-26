using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAfterSearching : StateMachineBehaviour
{

    private float initialTime = 0;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        ////Debug.Log("Duration "+stateInfo.length);
        initialTime = stateInfo.length;
    }


    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        var _soldierMachineState = animator.transform.GetComponent<SoldierMachineState>();

        initialTime -= ( Time.deltaTime * stateInfo.speedMultiplier );

        if(initialTime <= 0) {
            if(_soldierMachineState.ValidateState(SoldierStates.searching) &&
            _soldierMachineState.SearchingState.SearchingStateValues == SearchingStateValues.idle) {
                _soldierMachineState.SetState(_soldierMachineState.PatrolState);
            }
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        var _soldierMachineState = animator.transform.GetComponent<SoldierMachineState>();

        _soldierMachineState.SearchingState.SearchingStateValues = SearchingStateValues._none;
        _soldierMachineState.SearchingState.FinishSearching = false;
            }

}

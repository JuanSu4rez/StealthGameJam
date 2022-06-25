using UnityEngine;
using System.Collections;

public class SoldierIdle : StateMachineBehaviour
{
    private float initialTime = 0;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        ////Debug.Log("Duration "+stateInfo.length);
        initialTime = stateInfo.length;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        var _soldierMachineState = animator.transform.GetComponent<SoldierMachineState>();
        if(_soldierMachineState.PatrolState.PatrolStateValue == PatrolStates.inspecting) {
            initialTime -= Time.deltaTime;
            if(initialTime <= 0) {

                _soldierMachineState.PatrolState.PatrolStateValue = PatrolStates.locomotion;
            }
        }
    }
}

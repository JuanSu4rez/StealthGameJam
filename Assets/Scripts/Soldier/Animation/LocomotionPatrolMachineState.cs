using UnityEngine;
using System.Collections;

public class LocomotionPatrolMachineState : StateMachineBehaviour
{
    // Use this for initialization
    void Start() {
    }
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        var _soldierMachineState = animator.transform.GetComponent<SoldierMachineState>();
        var position = _soldierMachineState.PatrolState.GetPosition();
        if(position != null) {
            _soldierMachineState.LocomotionState.SetDestiny(position.Value);
            var soundsController = animator.transform.GetComponent<SoundsController>();
            if(soundsController) {
                soundsController.PlaySound();
            }
            animator.SetInteger("patrolState", (int)_soldierMachineState.PatrolState.PatrolStateValue);
        }
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        var _soldierMachineState = animator.transform.GetComponent<SoldierMachineState>(); 
        if(
            _soldierMachineState.PatrolState.PatrolStateValue == PatrolStates.locomotion 
            && !_soldierMachineState.LocomotionState.enabled
            ) {
            _soldierMachineState.PatrolState.NextPosition();
            _soldierMachineState.PatrolState.PatrolStateValue = PatrolStates.inspecting;
            animator.SetInteger("patrolState", (int)_soldierMachineState.PatrolState.PatrolStateValue);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        var _soldierMachineState = animator.transform.GetComponent<SoldierMachineState>();
        var soundsController = animator.transform.GetComponent<SoundsController>();
        if(soundsController) {
            soundsController.StopSound();
        }
    }
}

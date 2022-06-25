using UnityEngine;
using System.Collections;

public class LocomotionAttackingMachineState : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        var _soldierMachineState = animator.transform.GetComponent<SoldierMachineState>();
        var soundsController = animator.transform.GetComponent<SoundsController>();
        soundsController.PlayMachineGunSound();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      
        var _soldierMachineState = animator.transform.GetComponent<SoldierMachineState>();
        var colliderController = animator.transform.GetComponent<ColliderController>();

        if(!IsValidState(ref  _soldierMachineState)) {
            return;
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        var _soldierMachineState = animator.transform.GetComponent<SoldierMachineState>();
        var soundsController = animator.transform.GetComponent<SoundsController>();
        soundsController.StopMachineGunSound();
    }

    private bool IsReadyToAttack(ref SoldierMachineState _soldierMachineState) {
        return !_soldierMachineState.LocomotionState.enabled &&
               _soldierMachineState.LocomotionState.HasReachThePoint &&
               _soldierMachineState.AttackingState.AttackState == AttackingStatesValues.chasing;
    }

    private bool IsValidState(ref SoldierMachineState _soldierMachineState) {
        return _soldierMachineState.SoldierState == SoldierStates.attacking;
    }

}

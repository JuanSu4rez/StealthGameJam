using UnityEngine;
using System.Collections;

public class LocomotionAttackingMachineState : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        var _soldierMachineState = animator.transform.GetComponent<SoilderMachineState>();
        if(_soldierMachineState.AttackingState.AttackState == AttackingStatesValues.attacking
       ) {
            animator.SetTrigger("onPositionAttack");
        }
        else {
            _soldierMachineState.AttackingState.AttackState = AttackingStatesValues.chasing;
            _soldierMachineState.LocomotionState.SetDestiny(_soldierMachineState.AttackingState.PointToGo.Value, 4);
            animator.SetInteger("attackState", (int)_soldierMachineState.AttackingState.AttackState);
        }
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        var _soldierMachineState = animator.transform.GetComponent<SoilderMachineState>();
        if(IsReadyToAttack(ref _soldierMachineState)
           ) {
            _soldierMachineState.AttackingState.AttackState = AttackingStatesValues.attacking;
            animator.SetTrigger("onPositionAttack");
        }

    }
    private bool IsReadyToAttack(ref SoilderMachineState _soldierMachineState) {

        return _soldierMachineState.AttackingState.AttackState == AttackingStatesValues.chasing && !_soldierMachineState.LocomotionState.enabled;
    }
}

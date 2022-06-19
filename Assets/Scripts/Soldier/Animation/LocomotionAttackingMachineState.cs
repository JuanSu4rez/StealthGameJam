using UnityEngine;
using System.Collections;

public class LocomotionAttackingMachineState : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        var _soldierMachineState = animator.transform.GetComponent<SoldierMachineState>();
        if(_soldierMachineState.AttackingState.AttackState == AttackingStatesValues.attacking
       ) {
            animator.SetTrigger("onPositionAttack");
            _soldierMachineState.AttackingState.StartAttack();
        }
        else {
            _soldierMachineState.AttackingState.AttackState = AttackingStatesValues.chasing;
            _soldierMachineState.LocomotionState.SetDestiny(_soldierMachineState.AttackingState.PointToGo.Value, 4);
            animator.SetInteger("attackState", (int)_soldierMachineState.AttackingState.AttackState);
        }
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        var _soldierMachineState = animator.transform.GetComponent<SoldierMachineState>();
        if(IsReadyToAttack(ref _soldierMachineState)
           ) {
            _soldierMachineState.AttackingState.AttackState = AttackingStatesValues.attacking;
            animator.SetTrigger("onPositionAttack");
            _soldierMachineState.AttackingState.StartAttack();
        }

    }
    private bool IsReadyToAttack(ref SoldierMachineState _soldierMachineState) {

        return _soldierMachineState.AttackingState.AttackState == AttackingStatesValues.chasing && !_soldierMachineState.LocomotionState.enabled;
    }
}

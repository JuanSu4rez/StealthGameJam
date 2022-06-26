using UnityEngine;
using System.Collections;

public class AttackingShootingMachineState : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        var _soldierMachineState = animator.transform.GetComponent<SoldierMachineState>();
        var soundsController = animator.transform.GetComponent<SoundsController>();
        if(_soldierMachineState.AttackingState.AttackState == AttackingStatesValues.attacking) { 
            soundsController.PlayMachineGunSound();
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        var _soldierMachineState = animator.transform.GetComponent<SoldierMachineState>();
        var soundsController = animator.transform.GetComponent<SoundsController>();
         soundsController.StopMachineGunSound();
    }
}

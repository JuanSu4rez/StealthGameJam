using UnityEngine;
using System.Collections;

public class LocomotionAttackingMachineState : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        var soundsController = animator.transform.GetComponent<SoundsController>();
      
        soundsController.PlayWalkingSound();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      
    
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        var soundsController = animator.transform.GetComponent<SoundsController>();
        soundsController.StopMachineGunSound();
    }

  

}

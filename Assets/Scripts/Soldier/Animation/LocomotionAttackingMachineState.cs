using UnityEngine;
using System.Collections;

public class LocomotionAttackingMachineState : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        var _soldierMachineState = animator.transform.GetComponent<SoldierMachineState>();
        var soundsController = animator.transform.GetComponent<SoundsController>();

        soundsController.PlayMachineGunSound();
        /*
        if(_soldierMachineState.AttackingState.AttackState == AttackingStatesValues.attacking) {
            StopChasing(animator, ref _soldierMachineState);
            soundsController.PlayMachineGunSound();
        }
        else {

            float movingVelocity = 4;
            _soldierMachineState.AttackingState.AttackState = AttackingStatesValues.chasing;
            _soldierMachineState.LocomotionState.SetDestiny(_soldierMachineState.AttackingState.PointToGo.Value, movingVelocity);
            animator.SetInteger("attackState", (int)_soldierMachineState.AttackingState.AttackState);
            if(soundsController) {
                soundsController.PlayWalkingSound();
            }
        }*/
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      
        var _soldierMachineState = animator.transform.GetComponent<SoldierMachineState>();
        var colliderController = animator.transform.GetComponent<ColliderController>();

        if(!IsValidState(ref  _soldierMachineState)) {
            return;
        }

        /*
        if(IsReadyToAttack(ref _soldierMachineState)) {
            StopChasing(animator, ref _soldierMachineState);
        }
        else if(HasStumbleWithThePlayer(ref _soldierMachineState)) {

            ////Debug.Log("HasStumbleWithThePlayer ");
            StopChasing(animator, ref _soldierMachineState);
        } */ /**/

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

    private bool HasStumbleWithThePlayer(ref SoldierMachineState _soldierMachineState) {
        return _soldierMachineState.AttackingState.AttackState == AttackingStatesValues.chasing &&
               _soldierMachineState.AttackingState.IsOnDistanceToAttack();
    }

    private void StopChasing(Animator animator, ref SoldierMachineState _soldierMachineState) {
        if(_soldierMachineState.LocomotionState.enabled) {
            _soldierMachineState.LocomotionState.Stop();
        }

       // _soldierMachineState.AttackingState.StartAttack();
       // var soundsController = animator.transform.GetComponent<SoundsController>();
        //soundsController.PlayMachineGunSound();
    }
   

    private bool IsValidState(ref SoldierMachineState _soldierMachineState) {
        return _soldierMachineState.SoldierState == SoldierStates.attacking;
    }

}

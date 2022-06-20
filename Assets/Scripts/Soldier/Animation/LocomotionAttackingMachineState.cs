﻿using UnityEngine;
using System.Collections;

public class LocomotionAttackingMachineState : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        var _soldierMachineState = animator.transform.GetComponent<SoldierMachineState>();

        if(_soldierMachineState.AttackingState.AttackState == AttackingStatesValues.attacking) {
            StopChasing(animator, ref _soldierMachineState);
        }
        else {

            float movingVelocity = 4;
            _soldierMachineState.AttackingState.AttackState = AttackingStatesValues.chasing;
            _soldierMachineState.LocomotionState.SetDestiny(_soldierMachineState.AttackingState.PointToGo.Value, movingVelocity);
            animator.SetInteger("attackState", (int)_soldierMachineState.AttackingState.AttackState);
            var soundsController = animator.transform.GetComponent<SoundsController>();
            if(soundsController) {
                soundsController.PlaySound();
            }
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        var _soldierMachineState = animator.transform.GetComponent<SoldierMachineState>();
        var colliderController = animator.transform.GetComponent<ColliderController>();

        if(IsReadyToAttack(ref _soldierMachineState)) {
            StopChasing(animator, ref _soldierMachineState);
        }
       else if(HasStumbleWithThePlayer(ref _soldierMachineState)) {

            //Debug.Log("HasStumbleWithThePlayer ");
            StopChasing(animator, ref _soldierMachineState);
        }
        
    }

    private bool IsReadyToAttack(ref SoldierMachineState _soldierMachineState) {
        return !_soldierMachineState.LocomotionState.enabled &&
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
        animator.SetTrigger("onPositionAttack");
        _soldierMachineState.AttackingState.StartAttack();
    }

}

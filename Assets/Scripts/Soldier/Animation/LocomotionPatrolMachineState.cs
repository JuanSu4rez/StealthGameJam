﻿using UnityEngine;
using System.Collections;

public class LocomotionPatrolMachineState : StateMachineBehaviour
{
    // Use this for initialization
    void Start() {
    }
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        var _soldierMachineState = animator.transform.GetComponent<SoilderMachineState>();
        var position = _soldierMachineState.PatrolState.NextPosition();
        _soldierMachineState.LocomotionState.SetDestiny(position);
        animator.SetInteger("patrolState", (int)_soldierMachineState.PatrolState.PatrolStateValue);
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        var _soldierMachineState = animator.transform.GetComponent<SoilderMachineState>(); ;
        if(
            _soldierMachineState.PatrolState.PatrolStateValue == PatrolStates.locomotion &&!_soldierMachineState.LocomotionState.enabled
            ) {
            _soldierMachineState.PatrolState.PatrolStateValue = PatrolStates.inspecting;
            animator.SetTrigger("inspecting");
        }
    }
}
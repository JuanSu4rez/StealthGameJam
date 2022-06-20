using UnityEngine;
using System.Collections;

public class AttackingShootingMachineState : StateMachineBehaviour
{


    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
       var _soldierMachineState = animator.transform.GetComponent<SoldierMachineState>();
        var colliderController = animator.transform.GetComponent<ColliderController>();
        if(_soldierMachineState.SoldierState == SoldierStates.attacking &&
            !colliderController.IsOnVisionRange(_soldierMachineState.AttackingState.Player) &&
            !_soldierMachineState.AttackingState.IsOnDistanceToAttack()) {
            _soldierMachineState.SetState(_soldierMachineState.PatrolState);
        }


    }
}

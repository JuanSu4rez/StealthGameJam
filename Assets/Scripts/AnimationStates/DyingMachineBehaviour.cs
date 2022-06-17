using UnityEngine;
using System.Collections;

public class DyingMachineBehaviour : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //Notify the game the 
        Debug.Log("SomeOneHasDead");
        GameController.Instance.DeadNotification(animator.transform.gameObject);
    }
}

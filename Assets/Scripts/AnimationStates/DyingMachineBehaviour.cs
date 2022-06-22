using UnityEngine;
using System.Collections;

public class DyingMachineBehaviour : StateMachineBehaviour{

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //Notify the game the 
        //Debug.Log("SomeOneHasDead");
        if(GameController.Instance != null)
            GameController.Instance.DeadNotification(animator.transform.gameObject);
    }

    
}

using UnityEngine;
using System.Collections;

public class SoldierAnimationController : MonoBehaviour
{
    private Animator _animator;
    private SoilderMachineState _soldierMachineState;
    private SoldierAnimationStates lastState = SoldierAnimationStates._none;
    // Use this for initialization
    void Start() {
        _animator = this.GetComponent<Animator>();
        _soldierMachineState = this.GetComponent<SoilderMachineState>();
    }
    // Update is called once per frame
    void Update() {
        var soldierState = _soldierMachineState.soldierState();
        var animationState = SoldierAnimationStates.idle;
        switch(soldierState) {
            case SoldierStates._none:
                break;
            case SoldierStates.patrolling:
                animationState = SoldierAnimationStates.walking;
                break;
            case SoldierStates.attacking:
                break;
            case SoldierStates.searching:
                break;
            case SoldierStates.returningtopatroll:
                break;
        }
        if(animationState != lastState )
            _animator.SetInteger("state", (int)animationState);
        lastState = animationState;
    }

}

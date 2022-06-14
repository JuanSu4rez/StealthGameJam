using UnityEngine;
using System.Collections;

public class SoldierAnimationController : MonoBehaviour
{
    private Animator _animator;
    private SoilderMachineState _soldierMachineState;
    private SoldierStates lastState = SoldierStates._none;
    // Use this for initialization
    void Start() {
        _animator = this.GetComponent<Animator>();
        _soldierMachineState = this.GetComponent<SoilderMachineState>();
    }
    // Update is called once per frame
    void Update() {
        var soldierState = _soldierMachineState.SoldierState();
        if(soldierState != lastState )
            _animator.SetInteger("state", (int)soldierState);
        lastState = soldierState;
    }

}

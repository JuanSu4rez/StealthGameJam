using UnityEngine;
using System.Collections;

public class SoldierAnimationController : MonoBehaviour
{
    private Animator _animator;
    private SoldierMachineState _soldierMachineState;
    private SoldierStates lastState = SoldierStates._none;
    // Use this for initialization
    void Start() {
        _animator = this.GetComponent<Animator>();
        _soldierMachineState = this.GetComponent<SoldierMachineState>();
    }
    // Update is called once per frame
    void Update() {



        var soldierState = _soldierMachineState.SoldierState;
        //if(soldierState != lastState)
       _animator.SetInteger("state", (int)soldierState);
        lastState = soldierState;


        _animator.SetInteger("patrolState", (int)_soldierMachineState.PatrolState.PatrolStateValue);

        _animator.SetInteger("attackState", (int)_soldierMachineState.AttackingState.AttackState);

        _animator.SetInteger("searchingState", (int)_soldierMachineState.SearchingState.SearchingStateValues);
        //Debug.Log("Soldier state "+(int)soldierState);

        switch(soldierState) {
            case SoldierStates._none:
                break;
            case SoldierStates.attacking:
                Debug.DrawLine(this.transform.position, this.transform.position + Vector3.up * 5, Color.red);
                break;
            case SoldierStates.patrol:
                Debug.DrawLine(this.transform.position, this.transform.position + Vector3.up * 5, Color.blue);
                break;
            case SoldierStates.searching:
                Debug.DrawLine(this.transform.position, this.transform.position + Vector3.up * 5, Color.yellow);
                break;
        }
    }


    public void Disable() {
        _animator.SetFloat("animationMultiplier", 0);
    }
}

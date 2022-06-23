using UnityEngine;
using System.Collections;
using System.Linq;
using System;
using System.Collections.Generic;

public class SoldierMachineState : MonoBehaviour, ISoldierState,IDisabler
{
    public PatrolState PatrolState { get; set; }
    public AttackingState AttackingState { get; set; }
    public SearchingState SearchingState { get; set; }
    public LocoMotionState LocomotionState { get; set; }
    public SoldierStates SoldierState { get => _soldierState();}
    public Boolean PlayerIsAlive { get => PlayerConstants.IsAlive; }
    protected MonoBehaviour currentState = null;
    // Use this for initialization
    void Start() {
        AttackingState = this.GetComponent<AttackingState>();
        SearchingState = this.GetComponent<SearchingState>();
        LocomotionState = this.GetComponent<LocoMotionState>();
        PatrolState = this.GetComponent<PatrolState>();
        currentState = PatrolState;
        currentState.enabled = true;
    }
    // Update is called once per frame
    void Update() {
        
    }
    public SoldierStates _soldierState() {
        return ( currentState as ISoldierState ).SoldierState;
    }

    public void SetState<T>(T newState) where T : MonoBehaviour, ISoldierState {
        if(newState == currentState) {
            //Debug.Log("!ERROR!!!!! NEW STATE CAN NOT BE SAME AS THE CURRENT STATE ");
            //Debug.Log(newState.GetType().FullName);
            //Debug.Log("!ERROR!!!!! ");
        }

        if(newState !=  LocomotionState && LocomotionState) {
            LocomotionState.Stop();
        }
        if(currentState)
            currentState.enabled = false;
        currentState = newState;
        if(currentState)
            currentState.enabled = true;
    }

    public bool ValidateState(SoldierStates state) {
        return SoldierState == state;
    }

    public void Disable() {
        this.enabled = false;
        currentState.enabled = false;
        this.gameObject.GetComponent<SoldierAnimationController>().Disable();
    }

    public bool IsWalking() {
        bool iswaking = false;
        switch(currentState) {

            case AttackingState attacking:
                iswaking = attacking.AttackState == AttackingStatesValues.chasing;
                break;
            case PatrolState patrol:
                iswaking = patrol.PatrolStateValue == PatrolStates.locomotion;
                break;
        }
        return iswaking;
    }
}

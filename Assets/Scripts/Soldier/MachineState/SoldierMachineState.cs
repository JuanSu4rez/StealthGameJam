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
        if(LocomotionState) {
            LocomotionState.DisableLocomotion();
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
}

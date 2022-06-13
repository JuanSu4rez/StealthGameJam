using UnityEngine;
using System.Collections;
using System.Linq;
using System;
using System.Collections.Generic;

public class SoilderMachineState : MonoBehaviour
{
    public PatrolState PatrolState { get; set; }
    public AttackingState AttackingState { get; set; }
    public SearchingState SearchingState { get; set; }
    public LocoMotionState LocomotionState { get; set; }
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
    public SoldierStates SoldierState() {
        return ( currentState as ISoldierState ).SoldierState;
    }

    public void SetState<T>(T newState) where T : MonoBehaviour, ISoldierState {
        LocomotionState.enabled = false;
        currentState.enabled = false;
        currentState = newState;
        currentState.enabled = true;
    }

    public bool ValidateState(SoldierStates state) {
        return SoldierState() == state;
    }


}

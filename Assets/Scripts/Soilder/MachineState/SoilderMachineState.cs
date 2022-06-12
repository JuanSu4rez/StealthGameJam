using UnityEngine;
using System.Collections;
using System.Linq;
using System;
using System.Collections.Generic;

public class SoilderMachineState : MonoBehaviour
{
    private PatrolState _patrolState;
    private AttackingState _attackingState;
    private SearchingState _searchingState;
    private ReturningToPatrollState _returningToPatroll;
    protected MonoBehaviour currentState = null;
    // Use this for initialization
    void Start() {

        _patrolState = this.GetComponent<PatrolState>();
        _attackingState = this.GetComponent<AttackingState>();
        _searchingState = this.GetComponent<SearchingState>();
        _returningToPatroll = this.GetComponent<ReturningToPatrollState>();

        _attackingState.enabled = false;
        _searchingState.enabled = false;
        _returningToPatroll.enabled = false;

        currentState = _patrolState;
        currentState.enabled = true;
        _attackingState.enabled = false;
        _searchingState.enabled = false;
        _returningToPatroll.enabled = false;
    }
    // Update is called once per frame
    void Update() {
    }
    public SoldierStates soldierState() {
        return ( currentState as ISoldierState ).soldierState;
    }
}

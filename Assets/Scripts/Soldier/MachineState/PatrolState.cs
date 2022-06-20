using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : MonoBehaviour, ISoldierState
{
    public PatrolStates PatrolStateValue { get; set; } = PatrolStates._none;
    public SoldierStates SoldierState { get => SoldierStates.patrol; }
    public int IndexPosition { get; set; }
    [SerializeField]
    private Transform pointsContainer;
    private Transform[] _positionsToPatrol;
    private bool HasStarted = false;
    void Start() {
        if(pointsContainer) { 
        _positionsToPatrol = pointsContainer.Cast<Transform>().ToArray();
        }
        IndexPosition = 0;
    }

    public void NextPosition() {
        IndexPosition = ( IndexPosition + 1 ) % _positionsToPatrol.Length;
    }

    public Vector3? GetPosition() {
        if(!IsPatrolSateSetUp()) {
            return null;
        }
        PatrolStateValue = PatrolStates.locomotion;
    
        return _positionsToPatrol[IndexPosition % _positionsToPatrol.Length].position;
    }

    public bool IsPatrolSateSetUp() {
        return pointsContainer!= null;
    }

}
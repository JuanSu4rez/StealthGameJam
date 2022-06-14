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
        _positionsToPatrol = pointsContainer.Cast<Transform>().ToArray();
        IndexPosition = -1;
        this.enabled = false;
    }

    public Vector3 NextPosition() {
        PatrolStateValue = PatrolStates.locomotion;
        IndexPosition = ( IndexPosition + 1 ) % _positionsToPatrol.Length;
        return _positionsToPatrol[IndexPosition].position;
    }
}
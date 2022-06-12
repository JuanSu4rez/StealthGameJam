using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : MonoBehaviour, ISoldierState
{
    public SoldierStates soldierState { get => SoldierStates.patrolling; }
    [SerializeField]
    private Transform pointsContainer;
    private Transform[] _positionsToPatrol;
    private Transform _positionToGo;
    private int _currentPosition = 0;
    private NavMeshAgent goNavMeshAgent;
    private float distanceOfTolerance = 0.8f;
    void Start() {
        goNavMeshAgent = this.GetComponent<NavMeshAgent>();
        _positionsToPatrol =  pointsContainer.Cast<Transform>().ToArray();
        _currentPosition = 0;
        _positionToGo = _positionsToPatrol[_currentPosition];
        goNavMeshAgent.SetDestination(_positionToGo.position);
    }
    void Update() {
        var position = transform.position;
        float distanceToDestiny = Mathf.Abs(Vector3.Distance(position, _positionToGo.position));
        if(distanceToDestiny <= distanceOfTolerance) {
            transform.position = _positionToGo.position;
            goNavMeshAgent.ResetPath();
            goNavMeshAgent.SetDestination(NextPosition());
        }
    }
    private Vector3 NextPosition() {
        _currentPosition++;
        _currentPosition = _currentPosition % _positionsToPatrol.Length;
        _positionToGo = _positionsToPatrol[_currentPosition];
        return _positionToGo.position;
    }
}
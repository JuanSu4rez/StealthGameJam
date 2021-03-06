using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using System;

public class LocoMotionState : MonoBehaviour
{
    private Vector3 _positionToGo;
    private NavMeshAgent _goNavMeshAgent;
    private float distanceOfTolerance = 0.2f;
    private int stuckCounter = 0;
    private Vector3 lastPosition = Vector3.zero ;
    public bool HasReachThePoint = false;
    public bool IsMoving { get {
            return _goNavMeshAgent && _goNavMeshAgent.enabled;
        } }

    void Start() {
        _goNavMeshAgent = this.GetComponent<NavMeshAgent>();
        _goNavMeshAgent.enabled = false;
        lastPosition = this.transform.position;
    }
    void Update() {
        var position = transform.position;
        float distanceToDestiny = Mathf.Abs(Vector3.Distance(position, _positionToGo));
        if(distanceToDestiny <= distanceOfTolerance) {
            this.transform.position = _positionToGo;
            HasReachThePoint = true;
            Stop();
        }
        /*
        else {

            float lastPositionDistance = Mathf.Abs(Vector3.Distance(position, lastPosition)) * 1000.0f;
            if(lastPositionDistance <= 0.5f) {
                stuckCounter++;
            }
            else {
                stuckCounter = 0;
            }
            if(stuckCounter == 20) {
                HasReachThePoint = true;
                Stop();
            }
        }*/
        lastPosition = transform.position;
    }
    public void SetDestiny(Vector3 vector, float speed = 1.2f) {
        HasReachThePoint = false;
        stuckCounter = 0;
        _positionToGo = vector;
        this.transform.LookAt(new Vector3(_positionToGo.x, 0, _positionToGo.z));
        _goNavMeshAgent.enabled = true;
        _goNavMeshAgent.speed = speed;
        _goNavMeshAgent.SetDestination(vector);
        this.enabled = true;
    }

    public void Stop() {
        if(_goNavMeshAgent.enabled) {
            _goNavMeshAgent.ResetPath();
            _goNavMeshAgent.enabled = false;
        }
        this.enabled = false; ;
    }
}

﻿using UnityEngine;
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
    public bool IsMoving { get; internal set; }

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
            this.enabled = false;
        }
        else {
            float lastPositiondistance = Mathf.Abs(Vector3.Distance(position, lastPosition)) * 1000.0f;
            if(lastPositiondistance <= 0.5f) {
                stuckCounter++;
            }
            else {
                stuckCounter = 0;
            }
            if(stuckCounter == 10)
                this.enabled = false;
        }
        lastPosition = transform.position;
    }
    public void SetDestiny(Vector3 vector, float speed = 1.2f) {
        stuckCounter = 0;
        _positionToGo = vector;
        _goNavMeshAgent.enabled = true;
        _goNavMeshAgent.speed = speed;
        _goNavMeshAgent.SetDestination(vector);
        this.enabled = true;
    }
    private void OnDisable() {
        IsMoving = false;
    }
    private void OnEnable() {
        IsMoving = true;
    }

    internal void DisableLocomotion() {
        if(_goNavMeshAgent.enabled) {
            _goNavMeshAgent.ResetPath();
            _goNavMeshAgent.enabled = false;
        }
        this.enabled = false;
    }
}

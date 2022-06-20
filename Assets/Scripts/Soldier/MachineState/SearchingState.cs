using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchingState : MonoBehaviour, ISoldierState
{
    public SoldierStates SoldierState { get => SoldierStates.searching; }
    public bool rotating = false;
    private float counter = 0;
    private Vector3 _position = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(rotating) {
            counter  = 1;
            transform.LookAt(_position);
            rotating = false;
            enabled = false;
        }
    }

    internal void LookAt(Vector3 position) {
        _position = position;
        rotating = true;
        enabled = true;
    }
}

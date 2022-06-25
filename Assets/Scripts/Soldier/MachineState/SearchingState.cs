using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchingState : MonoBehaviour, ISoldierState
{
    public SoldierStates SoldierState { get => SoldierStates.searching; }
    
    private float counter = 0;
    private Vector3 _position = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void LookAt(Vector3 position) {
        this.transform.LookAt(position);
    }
}

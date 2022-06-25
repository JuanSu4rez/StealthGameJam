using UnityEngine;
using System.Collections;
using System;

public class HearingController : MonoBehaviour, IHearingHandler
{
    private SoldierMachineState _soldierMachineState;
   

    // Use this for initialization
    void Start() {
        _soldierMachineState = this.GetComponent<SoldierMachineState>();
    }

    // Update is called once per frame
    void Update() {

    }

    public void HandleHearing(Collider collider) {
        if(_soldierMachineState.ValidateState(SoldierStates.searching)) {
            return;
        }
      //  var angle = Vector3.SignedAngle(this.transform.forward, collider.transform.position, Vector3.up);
        _soldierMachineState.SearchingState.LookAt(collider.transform.position);
        //_soldierMachineState.SetState(_soldierMachineState.SearchingState);
        ////Debug.Log("OnSearchingState ");

    }

}

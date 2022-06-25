using UnityEngine;
using System.Collections;
using System;

public class HearingController : MonoBehaviour, IHearingHandler
{
    private SoldierMachineState _soldierMachineState;
    private MarksController _marksController;
    public float _Time = 2;
    // Use this for initialization
    void Start() {
        _soldierMachineState = this.GetComponent<SoldierMachineState>();
        _marksController = this.GetComponent<MarksController>();
    }

    // Update is called once per frame
    void Update() {
        if(
              _marksController.IsSearching &&
            !_soldierMachineState.ValidateState(SoldierStates.attacking) && _Time>0){
            _Time -= Time.deltaTime;
        }
        else{
            _marksController.IsSearching = false;
            _Time = 2;
        }
    }

    public void HandleHearing(Collider collider) {
        if(_soldierMachineState.ValidateState(SoldierStates.searching)) {
            return;
        }
      //  var angle = Vector3.SignedAngle(this.transform.forward, collider.transform.position, Vector3.up);
        _soldierMachineState.SearchingState.LookAt(collider.transform.position);
        _marksController.IsSearching = true;
        //_soldierMachineState.SetState(_soldierMachineState.SearchingState);
        ////Debug.Log("OnSearchingState ");

    }

}

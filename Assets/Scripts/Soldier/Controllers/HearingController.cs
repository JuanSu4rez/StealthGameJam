using UnityEngine;
using System.Collections;
using System;

public class HearingController : MonoBehaviour, IHearingHandler
{
    private SoldierMachineState _soldierMachineState;
    private MarksController _marksController;
    public float _Time = 2;
    public GameObject Player {
        get;
        set;
    }
    public bool IamSearching = false;
    // Use this for initialization
    void Start() {
        _soldierMachineState = this.GetComponent<SoldierMachineState>();
        _marksController = this.GetComponent<MarksController>();
    }

    // Update is called once per frame
    void Update() {
        if(_soldierMachineState.ValidateState(SoldierStates.searching)) {
            if(_soldierMachineState.LocomotionState.HasReachThePoint) {

                _soldierMachineState.SearchingState.SearchingStateValues = SearchingStateValues.idle;
                IamSearching = false;
                _marksController.IsSearching = false;
                _soldierMachineState.SearchingState.FinishSearching = true;
            }
        }


    }

    public void HandleHearing(Collider collider) {
        if(_soldierMachineState.ValidateState(SoldierStates.patrol)
            ||
            ( _soldierMachineState.ValidateState(SoldierStates.searching)
            )) {
            Player = collider.gameObject;
            //if there is no wall

            if(!PlayerIsBehindOfAWall()) {
                _soldierMachineState.SearchingState.LookAt(collider.transform.position);
            }
            else {
                IamSearching = true;
                _soldierMachineState.SearchingState.SearchingStateValues = SearchingStateValues.moving;
                if(!_soldierMachineState.ValidateState(SoldierStates.searching)) {
                    _soldierMachineState.SetState(_soldierMachineState.SearchingState);
                }
                _soldierMachineState.LocomotionState.SetDestiny(Player.transform.position,2.3f);
            }
            _marksController.IsSearching = true;
        }

    }

    public bool PlayerIsBehindOfAWall() {
        var controller = Player.GetComponent<PlayerController>();
        var result = false;
        if(controller != null) {
            result = controller.CollideWithObstacle(this.gameObject);
        }
        Debug.Log("PlayerIsBehindOfAWall " + result);
        return result;
    }

}

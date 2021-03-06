using UnityEngine;
using System.Collections;
using System;

public class AIEnemiesController : MonoBehaviour
{
    // Use this for initialization
    void Start() {

    }
    // Update is called once per frame
    void Update() {

    }

    public void SoldiersToPatroll() {
        ////Debug.Log("AIEnemiesController SoldiersToPatroll");
        int changedSoilders = 0;
        var soldiers = GetListOfSoilders();
        if(soldiers != null && soldiers.Length > 0) {
            ////Debug.Log("SoldiersToPatroll "+ soldiers.Length);
            foreach(var soldier in soldiers) {
                var _soldierMachineState = soldier.GetComponent<SoldierMachineState>();
                if(_soldierMachineState && _soldierMachineState.ValidateState(SoldierStates.attacking) && _soldierMachineState.AttackingState.AttackState == AttackingStatesValues.chasing) {
                    _soldierMachineState.SetState(_soldierMachineState.PatrolState);
                    changedSoilders++;
                }
            }
        }
        ////Debug.Log("SoldiersToPatrollEnd " + changedSoilders);
    }



    public void SoldiersToPatrollGameOver() {
        ////Debug.Log("AIEnemiesController SoldiersToPatroll");
        int changedSoilders = 0;
        var soldiers = GetListOfSoilders();
        if(soldiers != null && soldiers.Length > 0) {
            ////Debug.Log("SoldiersToPatroll "+ soldiers.Length);
            foreach(var soldier in soldiers) {
                var _soldierMachineState = soldier.GetComponent<SoldierMachineState>();
                if(!_soldierMachineState.ValidateState(SoldierStates.patrol)) {
                    _soldierMachineState.SetState(_soldierMachineState.PatrolState);
                    changedSoilders++;
                }
            }
        }
        ////Debug.Log("SoldiersToPatrollEnd " + changedSoilders);
    }

    public void PlayerIsSpotted(GameObject gameObject) {
        int changedSoilders = 0;
        var soldiers = GetListOfSoilders();
        if(soldiers != null && soldiers.Length > 0) {
            foreach(var soldier in soldiers) {
                var watchingController = soldier.GetComponent<WatchingController>();
                if(watchingController) {
                    watchingController.GoToAttack(gameObject);
                    changedSoilders++;
                }
            }
        }
        ////Debug.Log("PlayerIsSpottedEnd " + changedSoilders);
    }



    public void PlayerWins() {
        var soldiers = GetListOfSoilders();
        if(soldiers != null && soldiers.Length > 0) {
            foreach(var soldier in soldiers) {
                MonoBehaviour monoBehavoiur = soldier.GetComponent<ColliderController>();
                if(monoBehavoiur) { 
                    monoBehavoiur.enabled = false;
                }
                monoBehavoiur = soldier.GetComponent<HearingController>();
                if(monoBehavoiur) {
                    monoBehavoiur.enabled = false;
                }
                monoBehavoiur = soldier.GetComponent<WatchingController>();
                if(monoBehavoiur) {
                    monoBehavoiur.enabled = false;
                }
                monoBehavoiur = soldier.GetComponent<SoundsController>();
                if(monoBehavoiur) {
                    monoBehavoiur.enabled = false;
                }
                var _soldierMachineState = soldier.GetComponent<SoldierMachineState>();
                if(_soldierMachineState) {
                    _soldierMachineState.SetState(_soldierMachineState.PatrolState);
                }
            }
        }
        ////Debug.Log("PlayerIsSpottedEnd " + changedSoilders);
    }

    private GameObject[] GetListOfSoilders() {
        var soldiers = GameObject.FindGameObjectsWithTag("Soldier");
        ////Debug.Log("GetListOfSoilders " + soldiers!= null +" "+soldiers?.Length);
        return soldiers;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderController : MonoBehaviour
{
    private MeshCollider visionCollider;
    private SphereCollider sphereCollider;
    private HearingController hearingController;
    private WatchingController watchingController;
    private SoilderMachineState _soldierMachineState;
    // Start is called before the first frame update
    void Start() {
        visionCollider = this.GetComponent<MeshCollider>();
        sphereCollider = this.GetComponent<SphereCollider>();
        hearingController = this.GetComponent<HearingController>();
        watchingController = this.GetComponent<WatchingController>();
        _soldierMachineState = this.GetComponent<SoilderMachineState>();
    }
    // Update is called once per frame
    void Update() {

    }
    //Upon collision with another GameObject, this GameObject will reverse direction
    private void OnTriggerEnter(Collider other) {
        Debug.Log("OnTriggerEnter colliderController");
        var onvisionCollider = false;
        if(other.name == "Cyborg") {
           var playerController =  other.GetComponent<IPlayerController>();

            if(visionCollider != null) {
                //Debug.Log("Is Vision Collider " + ( other.bounds == visionCollider.bounds ));
                if(visionCollider.ClosestPointOnBounds(other.transform.position) == other.transform.position) {
                    //Debug.Log(other.name + " IS ON vision range");
                    onvisionCollider = true;
                }
            }
            if(onvisionCollider) {
                watchingController.HandleWatching(other);
            }
            else {

                var palyerisWalking = playerController.PlayerState == PlayerStates.walking ||
                                      playerController.PlayerState ==  PlayerStates.walkingCrouch;
                Debug.Log("OnTriggerEnter " + palyerisWalking+" "+ playerController.PlayerState);
                if(palyerisWalking) {
                   hearingController.HandleHearing(other);
                }
            }
        }
    }
    void OnTriggerStay(Collider other) {
        Debug.Log("OnTriggerEnter colliderController");
        var onvisionCollider = false;
        if(other.name == "Cyborg") {
            var playerController = other.GetComponent<IPlayerController>();

            if(visionCollider != null) {
                //Debug.Log("Is Vision Collider " + ( other.bounds == visionCollider.bounds ));
                if(visionCollider.ClosestPointOnBounds(other.transform.position) == other.transform.position) {
                    //Debug.Log(other.name + " IS ON vision range Stay");
                    onvisionCollider = true;
                }
            }
            if(onvisionCollider) {
                watchingController.HandleWatching(other);
            }
            else {

                var palyerisWalking = playerController.PlayerState == PlayerStates.walking ||
                                      playerController.PlayerState == PlayerStates.walkingCrouch;
                //Debug.Log("OnTriggerEnter " + palyerisWalking);
                if(palyerisWalking) {
                    hearingController.HandleHearing(other);
                }
            }
        }
    }
    void OnTriggerExit(Collider other) {
        Debug.Log("OnTriggerEnter colliderController");
        if(_soldierMachineState.ValidateState(SoldierStates.patrol)) {
            return;
        }
        if(other.name == "Cyborg") {
            //Debug.Log("OnTriggerExit");
            _soldierMachineState.PatrolState.PatrolStateValue = PatrolStates.locomotion;
            _soldierMachineState.SetState(_soldierMachineState.PatrolState);
        }
    }
}

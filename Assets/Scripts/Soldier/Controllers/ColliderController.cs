using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderController : MonoBehaviour
{
    private MeshCollider visionCollider;
    private SphereCollider sphereCollider;
    private HearingController hearingController;
    private WatchingController watchingController;
    private SoldierMachineState _soldierMachineState;
    

    // Start is called before the first frame update
    void Start() {
        visionCollider = this.GetComponent<MeshCollider>();
        sphereCollider = this.GetComponent<SphereCollider>();
        hearingController = this.GetComponent<HearingController>();
        watchingController = this.GetComponent<WatchingController>();
        _soldierMachineState = this.GetComponent<SoldierMachineState>();
    }
    // Update is called once per frame
    void Update() {
    }
    //Upon collision with another GameObject, this GameObject will reverse direction
    private void OnTriggerEnter(Collider other) {
        ////Debug.Log("OnTriggerEnter colliderController");
        HandleTrigger(other);
    }



    void OnTriggerStay(Collider other) {
        ////Debug.Log("OnTriggerEnter colliderController");
        HandleTrigger(other);
    }

    void OnTriggerExit(Collider other) {
        ////Debug.Log("OnTriggerEnter colliderController");
        Debug.DrawLine(other.transform.position, other.transform.position + Vector3.up * 5, Color.white);
        if(watchingController.IamAttacking ) {
            watchingController.HandleOnvisionExit(other);
        }
    }

   
    public bool IsOnVisionRange(GameObject gameObject) {
        if(visionCollider.ClosestPointOnBounds(gameObject.transform.position) == gameObject.transform.position) {
          //  Debug.Log(gameObject.name + " IS ON vision range");
            return true;
        }
        return false;
    }

    private void HandleTrigger(Collider other) {
        var onvisionCollider = false;
        if(other.name == "Cyborg") {
            var playerController = other.GetComponent<IPlayerController>();
            if(visionCollider != null) {
                ////Debug.Log("Is Vision Collider " + ( other.bounds == visionCollider.bounds ));
                onvisionCollider = IsOnVisionRange(other.transform.gameObject);

            }
            if(onvisionCollider) {
                //watchingController.IamAttacking = true;
                watchingController.HandleWatching(other);
            }
            else {
                //watchingController.IamAttacking = false;
               var playerIsRunning = playerController.PlayerState == PlayerStates.running;
                ////Debug.Log("OnTriggerEnter " + playerIsRunning+" "+ playerController.PlayerState);
                if(playerIsRunning) {
                    hearingController.HandleHearing(other);
                }
            }
        }
    }
}

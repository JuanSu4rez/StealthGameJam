using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPointController : MonoBehaviour
{
    void OnTriggerEnter(Collider other) {
        HandleTrigger( other);
    }

    private void HandleTrigger(Collider other) {
        if(other.name == "Cyborg") {
            GameController.Instance.PlayerWins();
        }
    }
}

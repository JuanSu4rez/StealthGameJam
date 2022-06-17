using UnityEngine;
using System.Collections;
using System;

public class GameController : MonoBehaviour
{
    public static GameController Instance = null;
    private GameController Player;
    // Use this for initialization
    void Start() {
        if(Instance == null) {
            Instance = this;
        }
        else{
            Destroy(this);
        }
    }

    internal void DeadNotification(GameObject gameObject) {
        if(Player == gameObject) {

        }
    }

    // Update is called once per frame
    void Update() {

    }
}

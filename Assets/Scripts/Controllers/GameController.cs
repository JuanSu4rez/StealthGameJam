using UnityEngine;
using System.Collections;
using System;

public class GameController : MonoBehaviour
{
    public static GameController Instance = null;
    public bool gameOver = false;
    public GameObject Player;
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
        Invoke("GameOver", 2.8f);
    }

    void GameOver() {
            gameOver = true;
    }
    // Update is called once per frame
    void Update() {
        if(gameOver) {
            Disable();
        }
    }
    void Disable() {

       
        this.enabled = false;
    }


}

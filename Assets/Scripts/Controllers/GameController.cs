using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip escapeSong;
    public static GameController Instance = null;
    public bool gameOver = false;
    private GameObject Player;    
    public bool playerIsSeen;
    public float lasTimePlayerWasSeen;
    public float lasTimePlayerWasOutOfTheLight;
    public AIEnemiesController _aiEnemiesController = null;
    public AIEnemiesController AIEnemiesController {
        get => _aiEnemiesController;
    }
    // Use this for initialization
    void Start() {
        PlayerConstants.IsAlive = true;
        Player = GameObject.FindGameObjectWithTag("Player");
        if(Player == null)
            throw new Exception("Player can not be null.");
        var objAiEnemiesController =  GameObject.Find("AIEnemiesController");
        _aiEnemiesController = objAiEnemiesController.GetComponent<AIEnemiesController>();
        if(Instance == null) {
            Instance = this;
        }
        else{
            Destroy(this);
        }
        audioSource = GetComponent<AudioSource>();
    }

    internal void DeadNotification(GameObject gameObject) {
        if(Player == gameObject) {
            PlayerConstants.IsAlive = false;
            //ShowGameOverAndTheProperButtons
            Invoke("SoldiersToPatroll", 2.0f);
            Invoke("GameOver", 2.8f);
        }
    }

    void GameOver() {
            gameOver = true;
        Invoke("LoadScene", 3);
    }
    void SoldiersToPatroll() {
        if(_aiEnemiesController) {
            _aiEnemiesController.SoldiersToPatroll();
        }
    }
    // Update is called once per frame
    void Update() {
        if(gameOver) {
            Disable();
        
        }
        if(playerIsSeen){
            PlayEscapeSong();
        } else {
            StopEscapeSong();}
    }
    void Disable() {
       // this.enabled = false;
    }
    void LoadScene() {
        //Debug.Log("LoadScene call");
        SceneManager.LoadScene(Scenes.SampleScene.ToString());
    }

    public void PlayEscapeSong(){
        audioSource.clip = escapeSong;
        if(!audioSource.isPlaying){
            audioSource.Play();
        }
    }

    public void StopEscapeSong(){
        audioSource.clip = escapeSong;
        audioSource.Stop();
    }

   

}

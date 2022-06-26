using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private AudioSource audioSourceAlarm;
    private AudioSource audioSourceMainTheme;
    public AudioClip escapeSong;
    public AudioClip mainTheme;
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
        if(Instance == null) {
            Instance = this;
        }
        else {
            Destroy(this);
        }
        PlayerConstants.IsAlive = true;
        Player = GameObject.FindGameObjectWithTag("Player");
        if(Player == null)
            throw new Exception("Player can not be null.");
        var objAiEnemiesController =  GameObject.Find("AIEnemiesController");
        _aiEnemiesController = objAiEnemiesController.GetComponent<AIEnemiesController>();
        audioSourceAlarm = gameObject.AddComponent<AudioSource>();        
        audioSourceAlarm.clip = escapeSong;
        audioSourceMainTheme = gameObject.AddComponent<AudioSource>();
        audioSourceMainTheme.clip = mainTheme;
        audioSourceMainTheme.loop = true;
        audioSourceMainTheme.volume = 0.3f;
        audioSourceAlarm.volume = 0.9f;
        //PlayMainSong();
    }

    internal void DeadNotification(GameObject gameObject) {
        if(Player == gameObject) {
            PlayerConstants.IsAlive = false;
            //ShowGameOverAndTheProperButtons
            Invoke("SoldiersToPatrollGameOver", 1.5f);
            Invoke("GameOver", 2.8f);
        }
    }

    void GameOver() {
        gameOver = true;
        Invoke("LoadScene", 3);
    }

    public void PlayerWins() {
        gameOver = true;
        var healthBehaviour = Player.GetComponent<HealthBehaviour>();
        if(healthBehaviour) {
            healthBehaviour.IsVulnerable = false;
        }
        MonoBehaviour behaviour = Player.GetComponent<HealthBar>();
        if(behaviour) {
            behaviour.enabled = false;
        }
        GameUIController.Instance.ShowWinMessge();
        Invoke("SoldiersToPatrollGameOver", 1.5f);
        if(_aiEnemiesController) {
            _aiEnemiesController.PlayerWins();
        }
    }

    void SoldiersToPatrollGameOver() {
        if(_aiEnemiesController) {
            _aiEnemiesController.SoldiersToPatrollGameOver();
        }
    }
    // Update is called once per frame
    void Update() {
        if(gameOver) {
            Disable();
            StopMainSong();
            StopEscapeSong();
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
        ////Debug.Log("LoadScene call");
        SceneManager.LoadScene(Scenes.SampleScene.ToString());
    }

    public void PlayEscapeSong(){
        if(!audioSourceAlarm.isPlaying){
            audioSourceAlarm.Play();
        }
    }

    public void StopEscapeSong(){        
        audioSourceAlarm.Stop();
    }

    public void PlayMainSong(){        
        if(!audioSourceMainTheme.isPlaying){
            audioSourceMainTheme.Play();
        }
    }

    public void StopMainSong(){        
        audioSourceMainTheme.Stop();        
    }
}

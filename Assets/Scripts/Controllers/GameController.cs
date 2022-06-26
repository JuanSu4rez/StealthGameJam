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
    public SpriteRenderer bulletSprite;
    public bool playerIsSeen;
    public float lasTimePlayerWasSeen;
    public float lasTimePlayerWasOutOfTheLight;
    public AIEnemiesController _aiEnemiesController = null;
    public AIEnemiesController AIEnemiesController {
        get => _aiEnemiesController;
    }
    private int showFirstRunningCourutine = 0;
    public int _firstRunCourutineWasRun = 0;
    public static bool staticfirstRunCourutineWasRun;
    public static int numberofdeads;
    private GameObject MainCamera;
    private GameObject BrainCamera;
    public GameObject ExitCamera;
    public int framecounter = 0;
    // Use this for initialization
    private void Awake() {
        if(_firstRunCourutineWasRun == 1) {
            staticfirstRunCourutineWasRun = true;
            showFirstRunningCourutine = 2;
        }

        if(Instance == null) {
            Instance = this;
        }
        else {
            Destroy(this);
        }
    }
    void Start() {
        ///
        PlayerConstants.IsAlive = true;
        ///
        Player = GameObject.FindGameObjectWithTag("Player");
        var controller = Player.GetComponent<PlayerController>();
        if(controller && !staticfirstRunCourutineWasRun) {
            controller.enabled = false;
        }
        ///
        MainCamera = GameObject.Find("Main Camera");
        BrainCamera = GameObject.Find("CM vcam1");
        ExitCamera.SetActive(false);
        ///
        if(staticfirstRunCourutineWasRun) {
            showFirstRunningCourutine = 2;
        }
        if(Player == null)
            throw new Exception("Player can not be null.");
        var objAiEnemiesController = GameObject.Find("AIEnemiesController");
        _aiEnemiesController = objAiEnemiesController.GetComponent<AIEnemiesController>();
        audioSourceAlarm = gameObject.AddComponent<AudioSource>();
        audioSourceAlarm.clip = escapeSong;
        audioSourceMainTheme = gameObject.AddComponent<AudioSource>();
        audioSourceMainTheme.clip = mainTheme;
        audioSourceMainTheme.loop = true;
        audioSourceMainTheme.volume = 0.3f;
        audioSourceAlarm.volume = 0.9f;
        PlayMainSong();

    }

    // Update is called once per frame
    void Update() {

        if(framecounter == 0) {
            if(numberofdeads > 4) {
                GameUIController.Instance.ShowEasyModeButton();
            }
            if(DamageConstants.IsInEasyMode()) {
                GameUIController.Instance.ShowEasyMode();
            }
        }

        if(showFirstRunningCourutine == 0) {
            StartCoroutine(shoWFristRunningCourutine());
        }

        if(showFirstRunningCourutine == 1) {
            return;
        }

        if(gameOver) {
            Disable();
            StopMainSong();
            StopEscapeSong();
        }
        if(playerIsSeen) {
            PlayEscapeSong();
        }
        else {
            StopEscapeSong();
        }
    }

    private void LateUpdate() {
        framecounter++;
    }

    private IEnumerator shoWFristRunningCourutine() {
        showFirstRunningCourutine = 1;
        GameUIController.Instance.ShowIntro();
        GameUIController.Instance.HideDefaultButtons();
        yield return new WaitForSeconds(5f);
        GameUIController.Instance.HideIntro();
        GameUIController.Instance.ShowGoal();
        BrainCamera.SetActive(false);
        MainCamera.SetActive(false);
        ExitCamera.SetActive(true);
        yield return new WaitForSeconds(5f);
        GameUIController.Instance.HideGoal();
        GameUIController.Instance.ShowControls();
        GameUIController.Instance.ShowDefaultButtons();
        BrainCamera.SetActive(true);
        MainCamera.SetActive(true);
        ExitCamera.SetActive(false);

        var controller = Player.GetComponent<PlayerController>();
        if(controller) {
            controller.enabled = true;
        }

        showFirstRunningCourutine = 2;
        staticfirstRunCourutineWasRun = true;
    }

    internal void DeadNotification(GameObject gameObject) {
        if(Player == gameObject) {
            numberofdeads++;
            PlayerConstants.IsAlive = false;
            //ShowGameOverAndTheProperButtons
            Invoke("SoldiersToPatrollGameOver", 0.8f);
            Invoke("GameOver", 2f);
        }
    }

    void GameOver() {
        gameOver = true;
        Invoke("LoadScene", 0.2f);
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

    void Disable() {
        // this.enabled = false;
    }
    void LoadScene() {
        ////Debug.Log("LoadScene call");
        SceneManager.LoadScene(Scenes.SampleScene.ToString());
    }

    public void PlayEscapeSong() {
        if(!audioSourceAlarm.isPlaying) {
            audioSourceAlarm.Play();
        }
    }

    public void StopEscapeSong() {
        audioSourceAlarm.Stop();
    }

    public void PlayMainSong() {
        if(!audioSourceMainTheme.isPlaying) {
            audioSourceMainTheme.Play();
        }
    }

    public void StopMainSong() {
        audioSourceMainTheme.Stop();
    }
}

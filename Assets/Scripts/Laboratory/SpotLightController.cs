using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotLightController : MonoBehaviour
{
    private GameObject alarm;
    private AlarmController alarmController;
    public GameController gameControllerScript;
    public GameObject player;
    void Awake()
    {
        alarm = GameObject.Find("Alarm");
        alarmController = alarm.GetComponent<AlarmController>();
        gameControllerScript = GameObject.Find("GameController").GetComponent<GameController>();
        player = GameObject.Find("Cyborg");
    }
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {        
        
      
    }
    
    void OnTriggerEnter(Collider other)
    {
        if(other.name.Equals("Cyborg") && player.GetComponent<PlayerController>().IsAlive()) {
            gameControllerScript.playerIsSeen = true;
            gameControllerScript.lasTimePlayerWasSeen = Time.realtimeSinceStartup;
            GameController.Instance.AIEnemiesController.PlayerIsSpotted(other.gameObject);
        }
    }
}

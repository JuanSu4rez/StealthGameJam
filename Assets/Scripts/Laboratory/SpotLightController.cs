using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotLightController : MonoBehaviour
{
    private GameObject alarm;
    private AlarmController alarmController;
    
    void Awake()
    {
        alarm = GameObject.Find("Alarm");
        alarmController = alarm.GetComponent<AlarmController>();
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
        if(other.name.Equals("Cyborg")){
            alarmController.playerIsSpotted = true;  
        }
    }
}

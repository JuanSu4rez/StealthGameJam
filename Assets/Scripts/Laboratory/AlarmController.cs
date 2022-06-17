using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmController : MonoBehaviour
{
    public GameObject alarmLight;
    private Light light;
    private Color originalColor;
    private float originalIntensity;
    public float finalIntensity;
    private float targetIntensity;
    private float speed;
    private bool lightGoingUp;
    private float tolerance;
    private AudioSource audioSource;
    public AudioClip alarmSound;
    public bool playerIsSpotted;

    void Awake()
    {
        light = alarmLight.GetComponent<Light>();
        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        originalColor = light.color;
        originalIntensity = light.intensity;
        finalIntensity = 1.5f;
        speed = 1.25f;
        lightGoingUp = true;
        tolerance = 0.3f;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerIsSpotted){
            turnOnAlarm();
        }
        else{
            turnOffAlarm();
        }
    }

    void turnOffAlarm(){
        light.color = originalColor;
        light.intensity = originalIntensity;
        audioSource.Stop();
    }

    void turnOnAlarm(){
        light.color = Color.red;        
        if(lightGoingUp){
            if(light.intensity < finalIntensity - tolerance){
                light.intensity = Mathf.Lerp(light.intensity, finalIntensity, Time.deltaTime * speed);
            }
            else{
                lightGoingUp = !lightGoingUp;
            }
        }
        else{
            if(light.intensity > originalIntensity + tolerance){
                light.intensity = Mathf.Lerp(light.intensity, originalIntensity, Time.deltaTime * speed);
            }
            else{
                lightGoingUp = !lightGoingUp;
            }
        }
        if(!audioSource.isPlaying){
            audioSource.PlayOneShot(alarmSound);
        }        
    }

   

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsController : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource audioSource;
    private SoldierMachineState _soldierMachineState;
    public AudioClip machineGun;
    public AudioClip walking;

    public AudioClip surprise;
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>(); 
        _soldierMachineState = this.GetComponent<SoldierMachineState>();
    }

    // Update is called once per frame
    void Update()
    {
        //TODO -- walking state ....
       /*if(!_soldierMachineState.IsWalking()) {
            StopWalkingSound();
        }    */    
    }

    public void PlayWalkingSound() {
        if(audioSource != null &&  !audioSource.isPlaying) {
            audioSource.clip = walking;
            audioSource.Play();
        }
    }

    public void StopWalkingSound() {
        if(audioSource != null &&  audioSource.isPlaying) {
            audioSource.clip = walking;
            audioSource.Stop();
        }
    }

     public void PlayMachineGunSound() {
        if(audioSource != null &&  !audioSource.isPlaying) {
            audioSource.clip = machineGun;
            audioSource.Play();
        }
    }

    public void StopMachineGunSound() {
        if(audioSource != null &&  audioSource.isPlaying) {
            audioSource.clip = machineGun;
            audioSource.Stop();
        }
    }

    public void PlaySurprisedSound() {
        if(audioSource != null &&  !audioSource.isPlaying) {
            audioSource.clip = surprise;
            audioSource.Play();
        }
    }
}

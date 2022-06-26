using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsController : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource audioSourceSteps;
    private AudioSource audioSourceMachineGun;
    private SoldierMachineState _soldierMachineState;
    public AudioClip machineGun;
    public AudioClip walking;

    public AudioClip surprise;
    void Start()
    {
       var audiosources = this.GetComponents<AudioSource>();
        audioSourceSteps = audiosources[0];
        audioSourceSteps.Stop();
        audioSourceMachineGun = audiosources[audiosources.Length-1];
        audioSourceMachineGun.Stop();
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
        if(audioSourceSteps != null &&  !audioSourceSteps.isPlaying) {
            audioSourceSteps.clip = walking;
            audioSourceSteps.Play();
        }
    }

    public void StopWalkingSound() {
        if(audioSourceSteps != null &&  audioSourceSteps.isPlaying) {
            audioSourceSteps.clip = walking;
            audioSourceSteps.Stop();
        }
    }

     public void PlayMachineGunSound() {
        if(audioSourceMachineGun != null &&  !audioSourceMachineGun.isPlaying) {
            audioSourceMachineGun.Play();
        }
    }

    public void StopMachineGunSound() {
        if(audioSourceMachineGun != null && audioSourceMachineGun.isPlaying) {
            audioSourceMachineGun.clip = machineGun;
            audioSourceMachineGun.Stop();
        }
    }

    public void PlaySurprisedSound() {
        if(audioSourceSteps != null &&  !audioSourceSteps.isPlaying) {
            audioSourceSteps.clip = surprise;
            audioSourceSteps.Play();
        }
    }
}

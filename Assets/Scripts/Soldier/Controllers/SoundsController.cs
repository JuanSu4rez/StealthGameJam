using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsController : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource audioSource;
    private SoldierMachineState _soldierMachineState;
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        _soldierMachineState = this.GetComponent<SoldierMachineState>();
    }

    // Update is called once per frame
    void Update()
    {
        //TODO -- walking state ....
       if(!_soldierMachineState.IsWalking()) {
            StopSound();
        }
    }

    public void PlaySound() {
        if(audioSource != null &&  !audioSource.isPlaying) {
            audioSource.Play();
        }
    }


    public void StopSound() {
        if(audioSource != null &&  audioSource.isPlaying) {
            audioSource.Stop();
        }
    }
}

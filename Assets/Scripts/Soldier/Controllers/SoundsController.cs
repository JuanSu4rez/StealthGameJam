using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsController : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource audioSource;
    private SoldierMachineState _soldierMachineState;
    private LocoMotionState locoMotionState;
    private SoldierAnimationController soldierAnimationController;

    private Animator _animator;



    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        _soldierMachineState = this.GetComponent<SoldierMachineState>();
        locoMotionState = this.GetComponent<LocoMotionState>();
        soldierAnimationController = this.GetComponent<SoldierAnimationController>();
        _animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //TODO -- walking state ....
        if(_soldierMachineState.ValidateState(SoldierStates.patrol) && locoMotionState.IsMoving) {
            if(!audioSource.isPlaying){
                audioSource.Play();
            }
        }
    }
}

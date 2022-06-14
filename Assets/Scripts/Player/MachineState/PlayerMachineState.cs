using UnityEngine;
using System.Collections;

public class PlayerMachineState : MonoBehaviour
{
    public StillState stillState;
    public WalkingState walkingState;
    protected MonoBehaviour currentState = null;
    // Use this for initialization
    void Start() {
        stillState = this.GetComponent<StillState>();
        walkingState = this.GetComponent<WalkingState>();
        SetState(stillState);
    }
    // Update is called once per frame
    void Update() {

    }
    public PlayerStates PlayerState() {
        return ( currentState as IPlayerState ).PlayerState;
    }

    public void SetState<T>(T newState) where T : MonoBehaviour, IPlayerState {
        if(currentState)
            currentState.enabled = false;
        currentState = newState;
        currentState.enabled = true;
    }

    
}

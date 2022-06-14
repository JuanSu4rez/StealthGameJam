using UnityEngine;
using System.Collections;

public interface IPlayerState{
    PlayerStates PlayerState { get;  }
}

public interface IPlayerController : IPlayerState {
    bool IsOnCrouchPosition { get; }
}
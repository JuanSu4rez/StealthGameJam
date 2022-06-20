using UnityEngine;
using System.Collections;

public interface IPlayerState{
    PlayerStates PlayerState { get;  }
}

public interface IPlayerController : IPlayerState, IDisabler{
    bool IsOnCrouchPosition { get; }
}
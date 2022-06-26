using UnityEngine;
using System.Collections;

public enum SoldierStates{
    _none,
    attacking,
    patrol,
    searching
}

public enum PatrolStates{
    _none,
    locomotion,
    inspecting
}

public enum AttackingStatesValues{
    _none,
    chasing,
    attacking
}


public enum SearchingStateValues
{
    _none,
    idle,
    moving
}



public enum SoldierAnimationStates{
    _none,
    idle,
    firing,
    searching,
    running,
    walking,
}

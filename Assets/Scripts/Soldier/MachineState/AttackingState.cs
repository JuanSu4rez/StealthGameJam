using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingState : MonoBehaviour, ISoldierState
{
    public SoldierStates SoldierState { get => SoldierStates.attacking; }
    public AttackingStatesValues AttackState { get; set; } = AttackingStatesValues._none;
    public Vector3? PointToGo { get; set; }
    public bool IsOnPositionToAttack { get; set; }
    private IWeaponBehaviour weaponBehaviour = null;
    [SerializeField]
    private GameObject weapon;
    public GameObject Player {
        get;
        internal set;
    }
    public GameObject Armor {
        get;
        internal set;
    }
    void Start() {
        this.GetComponent<IWeaponBehaviour>();
    }
    void Update() {
        if(PointToGo.HasValue)
            Debug.DrawLine(PointToGo.Value, PointToGo.Value + ( Vector3.up * 5 ), Color.red);
        if(AttackState == AttackingStatesValues.attacking) {
            //sent ray to validate if there is a wall
            this.transform.LookAt(Player.transform.position);
            Debug.DrawLine(this.transform.position, Player.transform.position, Color.cyan);
        }
    }

    public void StartAttack() {
        weaponBehaviour = weapon?.GetComponent<IWeaponBehaviour>();
        if(weaponBehaviour != null) {
            weaponBehaviour?.SetTarget(Player);
        }
    }

    public void OnDisable() {
        //this can be called from any of the StateMachineBehaviour 
        weaponBehaviour?.Disable();
    }
}

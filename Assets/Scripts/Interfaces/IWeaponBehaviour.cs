using UnityEngine;
using System.Collections;

public interface IWeaponBehaviour {
    bool IsActive { get; set; }
    void SetTarget(GameObject gameObject);
    void Disable();
}

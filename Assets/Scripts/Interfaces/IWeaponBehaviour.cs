using UnityEngine;
using System.Collections;

public interface IWeaponBehaviour {
    bool IsActive { get; }
    void SetTarget(GameObject gameObject);
    void Disable();
}

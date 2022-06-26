using UnityEngine;
using System.Collections;

public interface IDamageable {
    bool IsVulnerable { get; } 
    void ApplyDamage(float damage);
}

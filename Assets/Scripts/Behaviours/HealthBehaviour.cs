using UnityEngine;
using System.Collections;

public class HealthBehaviour : MonoBehaviour, IDamageable
{
    [SerializeField]
    private float MaxHealth =100;
    [SerializeField]
    private float Health;
    public bool IsAlive { get => Health >= 0; }
    // Use this for initialization
    void Start() {
        this.enabled = false;
        Health = MaxHealth;
    }

    public void ApplyDamage(float damage) {
        Health -= damage;
    }
}

using UnityEngine;
using System.Collections;

public class HealthBehaviour : MonoBehaviour, IDamageable{
    [SerializeField]
    private float maxHealth =100;
    public float MaxHealth { get => maxHealth;  }
    [SerializeField]
    private float health;
    public float Health { get => health; }
    public bool IsAlive { get => health >= 0; }
    public float Percentage { get => health / maxHealth; }
    // Use this for initialization
    void Start() {
        this.enabled = false;
        health = MaxHealth;
    }

    public void ApplyDamage(float damage) {
        health -= damage;
    }
}

using UnityEngine;
using System.Collections;

public class HealthBehaviour : MonoBehaviour, IDamageable
{
    [SerializeField]
    private readonly float MaxHealth =100;
    private float Health;
    public bool IsAlive { get; private set; }
    // Use this for initialization
    void Start() {
        this.enabled = false;
        Health = MaxHealth;
    }

    public void ApplyDamage(float damage) {
        Health -= damage;
        IsAlive = Health >= 0;
    }
}

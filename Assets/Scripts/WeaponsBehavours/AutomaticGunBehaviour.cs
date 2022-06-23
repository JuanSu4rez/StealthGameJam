using UnityEngine;
using System.Collections;

public class AutomaticGunBehaviour : MonoBehaviour, IWeaponBehaviour
{
    public float Damage = 0.5f;
    public float Frecuency = 0.3f;
    public IDamageable _target;
    private GameObject targetgo = null;
    private bool flag = false;

    public bool IsActive { get; set; }

    // Use this for initialization
    void Start() {
        this.enabled = false;
    }

    IEnumerator DamageRutine() {
        yield return new WaitForSeconds(0.01f);
        while(flag) {
            Debug.DrawLine(transform.position, targetgo.transform.position, Color.cyan);
            yield return new WaitForSeconds(Frecuency);
            _target.ApplyDamage(Damage);
        }
        Debug.Log("End DamageRutine");
    }

    public void SetTarget(GameObject gameObject) {
        targetgo = gameObject;
        var target = gameObject?.GetComponent<IDamageable>();
        if(target != null) {
            _target = target;
            this.enabled = true;
            this.flag = true;
            StartCoroutine(DamageRutine());
        }
    }

    public void Disable() {
        this.flag = false;
        this.enabled = false;
    }

}

using UnityEngine;
using System.Collections;

public class AutomaticGunBehaviour : MonoBehaviour, IWeaponBehaviour
{
    public float Damage = 0.5f;
    public float Frecuency = 0.3f;
    public IDamageable _target;
    private bool flag = false;
    private IEnumerator coroutine = null;

    public bool IsActive { get;set; }

    // Use this for initialization
    void Start() {
        this.enabled = false;
        coroutine = DamageRutine();
    }

    IEnumerator DamageRutine() {
        while(flag) {
            yield return new WaitForSeconds(Frecuency);
            _target.ApplyDamage(Damage);
        }
    }

    public void SetTarget(GameObject gameObject) {
        var target = gameObject?.GetComponent<IDamageable>();
        if(target != null) {
            _target = target;
            this.enabled = true;
            this.flag = true;
            StartCoroutine(coroutine);
        }
    }

    public void Disable() {
        StopCoroutine(coroutine);
        this.flag = false;
        this.enabled = false;
    }

}

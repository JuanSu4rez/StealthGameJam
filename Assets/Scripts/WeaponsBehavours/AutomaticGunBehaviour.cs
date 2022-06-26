using UnityEngine;
using System.Collections;

public class AutomaticGunBehaviour : MonoBehaviour, IWeaponBehaviour
{
    private float Damage = DamageConstants.Damage;
    private float Frecuency = DamageConstants.Frecuency;
    public IDamageable _target;
    private GameObject targetGobject = null;
    public GameObject TargetGameobject { get => targetGobject; }
    private bool flag = false;

    public bool IsActive { get => flag; }
    void Awake() {
        gameObject.AddComponent<AutomaticGunRenderer>();
    }

    // Use this for initialization
    void Start() {
        this.enabled = false;
    }

    IEnumerator DamageRutine() {
        yield return new WaitForSeconds(0.01f);
        while(flag) {
            Debug.DrawLine(transform.position, targetGobject.transform.position, Color.cyan, 3);
            yield return new WaitForSeconds(Frecuency);
            if( _target.IsVulnerable ) {
                _target.ApplyDamage(Damage);
            }
        }
        //Debug.Log("End DamageRutine");
    }

    public void SetTarget(GameObject gameObject) {
        targetGobject = gameObject;
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

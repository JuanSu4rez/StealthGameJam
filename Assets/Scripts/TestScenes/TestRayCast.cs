using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TestRayCast : MonoBehaviour
{
    private CapsuleCollider[] capsuleColliders;
    private CapsuleCollider collider;
    public LayerMask LayerMask;
    public GameObject gameObject;
    // Start is called before the first frame update
    void Start() {
        capsuleColliders = this.GetComponents<CapsuleCollider>();
        Debug.Log(System.Convert.ToString(( (int)LayerMask ), 2));
    }

    // Update is called once per frame
    void Update() {
        collider = GetActiveCollider();
        var maxdistance = 15;
        if(collider) {



            Debug.Log("Hit wall! " + CollideWithWall(gameObject));
        }
    }

    CapsuleCollider GetActiveCollider() {
        return capsuleColliders.FirstOrDefault(p => p.enabled);
    }

    public bool CollideWithWall(GameObject reference) {
        var origin = this.transform.position + ( ( collider.bounds.extents.z ) * transform.TransformDirection(Vector3.forward) ) + ( collider.bounds.extents.y * Vector3.up );
        var gopos = gameObject.transform.position;
        var distance = gopos - origin;
        Debug.DrawLine(gopos, origin, Color.green);
        RaycastHit[] hits = Physics.RaycastAll(origin, ( distance ).normalized, distance.magnitude, LayerMask);
        if(hits.Length > 0) {
            var values = hits.Select(p => p.collider.gameObject.name);
            Debug.Log("Object hit " + hits.Length + string.Join(";", values));
            return true;
        }
        return false;
    }
}

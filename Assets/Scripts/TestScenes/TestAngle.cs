using UnityEngine;
using System.Collections;

public class TestAngle : MonoBehaviour
{

    public GameObject A;
    public GameObject B;
    public GameObject C;
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        var origin = this.transform.position;
        var objforward = this.transform.position + this.transform.forward;
        Debug.DrawLine(this.transform.position, objforward, Color.blue);
        var direction =  A.transform.position - this.transform.position;
        Debug.DrawLine(this.transform.position, direction, Color.cyan);
        var angle = Vector3.SignedAngle(objforward, direction, Vector3.up);
        Debug.Log("Angle "+ angle);
        //Debug.Log("Angle +360 " + (360 + angle));
    }
}

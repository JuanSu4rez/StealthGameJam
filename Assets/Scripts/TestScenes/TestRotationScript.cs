using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRotationScript : MonoBehaviour
{
    public GameObject A;
    public GameObject B;
    public GameObject C;
    private int rotation = 0;
    public bool rotating = false;
    Quaternion from;
    Quaternion to;
    float counter = 0;
    float timeCount = 0;
    float speed = 0.1f;
    float angle = 0;
    float myangle = 0;
    float cross = 0;
    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        var quaternion = this.transform.rotation;
        Debug.Log("quaternion " + quaternion.eulerAngles.y);
        var forward = Vector3.forward;
        Debug.DrawLine(this.transform.position, this.transform.position + this.transform.forward, Color.blue);
        Debug.DrawLine(this.transform.position, forward, Color.green);
        Debug.DrawLine(this.transform.position, A.transform.position, Color.red);
        //Debug.DrawLine(this.transform.position, B.transform.position, Color.red);
        //Debug.DrawLine(this.transform.position, C.transform.position, Color.red);

        if(!rotating) {
            var calculated = A.transform.position - this.transform.position;
            angle = (int)Vector3.SignedAngle(forward, calculated, Vector3.up);
            myangle = (int)Vector3.SignedAngle(forward, A.transform.position, Vector3.up);
            cross = Mathf.Sign(angle);
            Debug.Log(cross);
            Debug.Log(angle);
            rotating = true;
        }


        if(rotating) {
            myangle = (int)(myangle+ cross);
            transform.rotation = Quaternion.AngleAxis(myangle, Vector3.up);
            rotating = myangle == angle ? false: true;
        }
    }
}

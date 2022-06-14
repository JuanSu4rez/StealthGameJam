using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestRotationScript : MonoBehaviour
{
    public GameObject A;
    public GameObject B;
    public GameObject C;
    private float timeCount = 0.0f;
    Quaternion to;
    private float angle = 0;
    private void Start() {

        angle = calculate(); 
        this.to = Quaternion.AngleAxis(angle, Vector3.up);
    }

    private int calculate() {
        var calculated = A.transform.position - this.transform.position;
        var forward = Vector3.forward;
        return (int)Vector3.SignedAngle(forward, calculated, Vector3.up);
    }

    void Update() {
        transform.rotation = Quaternion.Slerp(this.transform.rotation, to, timeCount);
        timeCount = timeCount + Time.deltaTime;
        var newangle = calculate();
        if(newangle != angle) {
            angle = newangle;
            this.to = Quaternion.AngleAxis(angle, Vector3.up);
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestRotationScript : MonoBehaviour
{
    public GameObject A;
    public GameObject B;
    public GameObject C;
    bool rotating = false;
    void Start() {
        StartRotationCourutine();
    }
    void Update() {
        Debug.DrawRay(this.transform.position, this.transform.forward, Color.blue);
    }
    void StartRotationCourutine() {
        var calculated =  this.transform.position - A.transform.position;
        var angle = (int)Vector3.SignedAngle(this.transform.forward, calculated, Vector3.up);
        var myAngle = this.transform.rotation.eulerAngles.y;
        Debug.Log(angle + " " + myAngle);
        var to = Quaternion.AngleAxis(angle, Vector3.up);
        StartCoroutine(LerpFunction(to, 5));

    }
    IEnumerator LerpFunction(Quaternion endValue, float duration) {
        rotating = true;
        yield return new WaitForSeconds(0.2f);
        float time = 0;
        Quaternion startValue = transform.rotation;
        while(time < duration) {
            transform.rotation = Quaternion.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.rotation = endValue;
        rotating = false;
    }
}

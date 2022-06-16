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
        if(!rotating) {
            StartCoroutine(LerpFunction(transform.rotation, 5));
        }
    }
    void StartRotationCourutine() {
        
        StartCoroutine(LerpFunction(transform.rotation, 5));

    }
    IEnumerator LerpFunction(Quaternion startValue, float duration) {
        rotating = true;
        
        float time = 0;
        var initialAngle = startValue.eulerAngles.y;
        if(initialAngle < 0)
            initialAngle += 360;
        
        var newAngle = initialAngle + UnityEngine.Random.Range(100, 180);

        while(time < duration) {
            var current = Mathf.Lerp(initialAngle, newAngle, time / duration);
            transform.rotation = Quaternion.AngleAxis(current, Vector3.up);
            time += Time.deltaTime;
            yield return null;
        }
        transform.rotation = Quaternion.AngleAxis(newAngle, Vector3.up);
   
        StartCoroutine(WaitRandomly());
    }
    IEnumerator WaitRandomly() {
        yield return new WaitForSeconds(UnityEngine.Random.Range(2, 10));
        rotating = false;
      
    }

}

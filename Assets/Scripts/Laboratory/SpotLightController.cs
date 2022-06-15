using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotLightController : MonoBehaviour
{
    private Light spotLight;
    private Quaternion spotInitialRotation;
    void Awake()
    {
        spotLight = GetComponent<Light>();
    }
    void Start()
    {
       spotInitialRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.rotation.x > 0.6f){
            transform.Rotate(-Vector3.right);
        }
        
      
    }
}

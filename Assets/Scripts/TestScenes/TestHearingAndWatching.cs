using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHearingAndWatching : MonoBehaviour
{
    public GameObject Soldier;
    public float AnimationMutiplier = 10;
    // Start is called before the first frame update
    void Start()
    {
       var _animator = Soldier.GetComponent<Animator>();
        _animator.SetFloat("animationMultiplier", AnimationMutiplier);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

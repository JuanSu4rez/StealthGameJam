using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRayCastCyborg : MonoBehaviour
{
    public GameObject ref1;
    public GameObject ref2;
    public GameObject ref3;
    private PlayerController playerController;
    // Start is called before the first frame update
    void Start() {
        playerController = this.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
         playerController.CollideWithObstacle(this.ref1);
         playerController.CollideWithObstacle(this.ref2);
         playerController.CollideWithObstacle(this.ref3);
    }
}

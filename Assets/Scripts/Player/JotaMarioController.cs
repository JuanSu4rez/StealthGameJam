using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JotaMarioController : MonoBehaviour{

    public float movementSpeed;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start() {
        rb = gameObject.GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update() {
        MovePlayer();
    }
    void MovePlayer() {
        if(Input.GetButton("Vertical") || Input.GetButton("Horizontal")) {
            float verticalInput = Input.GetAxis("Vertical");
            float horizontalInput = Input.GetAxis("Horizontal");
            var vector2 = new Vector2(-horizontalInput, verticalInput);
            var angle = Vector2.SignedAngle(Vector2.up, vector2);
            //Debug.Log(angle + " " + vector2);
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
            var movement = ( transform.forward * verticalInput ) +
                           ( transform.right * -horizontalInput );
            transform.Translate(movement * Time.deltaTime * movementSpeed);
        }
    }
}

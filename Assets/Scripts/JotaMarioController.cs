using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JotaMarioController : MonoBehaviour
{
    public float movementSpeed;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
     rb = gameObject.GetComponent<Rigidbody>();   
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    void MovePlayer(){
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput) * Time.deltaTime * movementSpeed;
        transform.Translate(movementDirection);
        rb.rotation = Quaternion.LookRotation(movementDirection, Vector3.up);

    }

    
}

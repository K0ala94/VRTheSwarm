using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


    public float playerSpeed;
    public float lookSpeed;
    private Rigidbody rb;

	void Start () {
        rb = GetComponent<Rigidbody>();
        CanMove = true;
	}
	
	
	void Update () {
        float zAxis = Input.GetAxis("Vertical")*playerSpeed*Time.deltaTime;
        float xAxis = Input.GetAxis("Horizontal") * playerSpeed * Time.deltaTime;

        float rotateHorizontally = Input.GetAxis("Mouse X");

        //transform.Rotate(0,rotateHorizontally*lookSpeed, 0);
        if(CanMove)
            transform.Translate(xAxis, 0, zAxis);

	}

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space) && CanMove)
        {
            rb.AddForce(transform.up* 200f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }


    public bool CanMove { private get; set; }

}

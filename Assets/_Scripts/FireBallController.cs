using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallController : MonoBehaviour {

    private GameObject DDController;
    public bool shooting = false;
    private float swingStartTime;
    private bool swinging = false;

    void Start () {
        DDController = GameObject.Find("GvrControllerPointer");
	}
	
	
	void Update () {
        if(!shooting)
            transform.position = DDController.transform.position + DDController.transform.forward * 1; 
	}

    private void FixedUpdate()
    {
       //controller szogsebessege radianban
        if (!shooting && GvrControllerInput.Gyro.x > 3.14f)
        {
            if (!swinging)
            {
                swingStartTime = Time.time;
                swinging = true;
            }
            else if (Time.time - swingStartTime > 0.15f)
            {
                shoot();
            }
        }
        else
        {
            swinging = false;
        }


    }

    public void shoot()
    {
        shooting = true;
        Transform camera = GameObject.Find("VrCamera").transform;
        Quaternion origRot = camera.rotation;
        camera.Rotate(new Vector3(-20f, 0, 0));
        GetComponent<Rigidbody>().velocity = camera.forward * GvrControllerInput.Gyro.x* 2;
        camera.rotation = origRot;
        Destroy(gameObject, 3);
    }

    private void OnTriggerEnter(Collider other)
    {
        //if(!other.gameObject.CompareTag("Player") && !other.gameObject.CompareTag("PlayerInteract"))
         //   Destroy(gameObject,0.1f);
        
    }
}

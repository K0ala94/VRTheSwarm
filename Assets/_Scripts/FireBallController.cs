using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallController : MonoBehaviour {

    private GameObject DDController;
    public bool shooting = false;

	void Start () {
        DDController = GameObject.Find("GvrControllerPointer");
	}
	
	
	void Update () {
        if(!shooting)
            transform.position = DDController.transform.position + DDController.transform.forward * 1; 
	}

    private void FixedUpdate()
    {
        if (!shooting && GvrControllerInput.Gyro.y > 3.14f)
        {
            shoot();
        }
    }

    public void shoot()
    {
        shooting = true;
        Transform camera = GameObject.Find("VrCamera").transform;
        Quaternion origRot = camera.rotation;
        camera.Rotate(new Vector3(-20f, 0, 0));
        GetComponent<Rigidbody>().velocity = camera.forward * 15;
        camera.rotation = origRot;
        Destroy(gameObject, 3);
    }

    private void OnTriggerEnter(Collider other)
    {
        //if(!other.gameObject.CompareTag("Player") && !other.gameObject.CompareTag("PlayerInteract"))
         //   Destroy(gameObject,0.1f);
        
    }
}

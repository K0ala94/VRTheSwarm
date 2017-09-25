using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelController : MonoBehaviour {

    private Rigidbody rb;
    private GameManager gameManager;
	void Start () {
        rb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (gameManager.Phase1 || gameManager.Phase2)
        {
            rb.angularVelocity = Random.insideUnitSphere * 5;
            transform.LookAt(GameObject.Find("Player").transform);
            transform.Rotate(new Vector3(-60, 0, 0));
            rb.AddForce(transform.forward * 460);
        }
	}
	
	void Update () {
		
	}
}

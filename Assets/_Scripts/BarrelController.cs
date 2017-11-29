using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelController : MonoBehaviour {

    private Rigidbody rb;
    private GameManager gameManager;
    public int damage = 20;
	void Start () {
        rb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (gameManager.Phase1 || gameManager.Phase2)
        {
            transform.LookAt(GameObject.Find("Player").transform);
            transform.Rotate(new Vector3(-55, 0, 0));
            rb.angularVelocity = Random.insideUnitSphere * 5;
            rb.AddForce(transform.forward * 450);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerBody") && (gameManager.Phase1 || gameManager.Phase2))
        {
            GameObject.Find("Player").GetComponent<PlayerHealth>().decreaseHealth(damage);
        }
    }

    void Update () {
		
	}
}

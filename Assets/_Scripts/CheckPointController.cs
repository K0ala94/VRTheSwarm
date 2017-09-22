using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointController : MonoBehaviour {

	void Start () {

        transform.LookAt(GameObject.Find("Player").transform);
	}

	void Update () {	
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("RuneEnd"))
        {
            Destroy(gameObject, 0.5f);
            RuneController runeController = other.GetComponentInParent<RuneController>();
            runeController.evaluateRunePerformance();
            other.enabled = false;
        }
        else if(other.tag != "PLayer")
        {
            if (other.gameObject.GetComponent<RuneController>() != null)
            {
                other.gameObject.GetComponent<RuneController>().checkPointCount++;
                other.enabled = false;
            }
        }
    }
}

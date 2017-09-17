using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogeController : MonoBehaviour {

    public GameObject player;

	void Start () {
        player = GameObject.Find("Player");
        transform.LookAt(player.transform);
        transform.Rotate(new Vector3(0, 180, 0));
    }
	
	
	void Update () {
        
        
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDisplayController : MonoBehaviour {

    private GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
        Destroy(gameObject, 5.0f);
    }

    void Update () {
        transform.LookAt(player.transform);
        transform.Rotate(new Vector3(0, 180, 0));
        transform.position += new Vector3(0, 0.03f, 0);
	}
}

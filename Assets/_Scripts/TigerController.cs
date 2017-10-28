using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TigerController : MonoBehaviour {

    private Animator tigerAnim;
    private bool following = true;
    private GameObject player;

	void Start () {
        player = GameObject.Find("Player");
        tigerAnim = GetComponent<Animator>();
        tigerAnim.SetBool("Follow", true);
	}
	
	
	void Update () {
        if (Vector3.Distance(transform.position, player.transform.position) < 3.5f)
        {
            tigerAnim.SetBool("Follow", false);
            following = false;
        }
        
        if (following)
        {
            Quaternion origDir = transform.rotation;
            transform.LookAt(player.transform);
            Quaternion destDir = transform.rotation;
            transform.rotation = origDir;

            transform.rotation =  Quaternion.Lerp(transform.rotation, destDir, 0.08f);
            transform.position += transform.forward * Time.deltaTime * 2.8f;
        }
		
	}

    

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerInteract"))
        {
            tigerAnim.SetBool("Follow", true);
            following = true;
        }
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizzardController : MonoBehaviour {

    private GameObject player;
    public  GameObject dialogePrefab;
    public Transform dialogeSpawnPoint;
    private GameObject dialoge;
    private GameObject home;
    private GameManager gameManager;
    private bool questAccepted = false;
    private bool dialogeActive = false;

	void Start () {
        player = GameObject.Find("Player");
        home = GameObject.Find("OgreDoor");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
	
	
	void Update () {
        if(!questAccepted)
            transform.LookAt(player.transform);
        else
        {
            transform.LookAt(home.transform);
            transform.position += transform.forward *Time.deltaTime* 0.9f;
        }
        if(dialogeActive && (Input.GetKeyDown(KeyCode.Space) || GvrController.ClickButtonDown || GvrController.TouchDown)){
            AcceptQuest(); 
        }

	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerInteract") && !questAccepted)
        {
            dialoge = (GameObject)Instantiate(dialogePrefab, dialogeSpawnPoint.position , Quaternion.identity);
            dialogeActive = true;
            player.GetComponent<VRPlayerController>().CanMove = false;
            player.GetComponent<VRPlayerController>().move = false;
        }
        else if (other.tag.Equals("WizzardsHome"))
        {
            Destroy(gameObject, 0.1f);
        }
    }

    public void AcceptQuest()
    {
        DestroyImmediate(dialoge);
        questAccepted = true;
        dialogeActive = false;
        player.GetComponent<VRPlayerController>().CanMove = true;
        gameManager.spawnKittens();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatOwnerController : MonoBehaviour {

    public Transform standPoint;
    private GameObject player;
    public GameObject tigerPrefab;
    public GameObject smokePrefab;
    private RuneManager runeManager;
    private bool dialogeActive = false;
    private bool dialogeDone = false;

	void Start () {
        player = GameObject.Find("Player");
        runeManager = GameObject.Find("RuneManager").GetComponent<RuneManager>();  
    }
	
	
	void Update () {
        transform.position = Vector3.MoveTowards(transform.position, standPoint.position, Time.deltaTime*1);
        transform.LookAt(player.transform);
        if (dialogeActive)
        {
            if ((Input.GetKeyDown(KeyCode.Space) || GvrController.ClickButtonDown || GvrController.TouchDown))
            {
                dialogeActive = false;
                dialogeDone = true;
                createSmoke();
                runeManager.spawnRune(runeManager.currentRune++);
               
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!dialogeDone)
            {
                dialogeActive = true;
                player.GetComponent<VRPlayerController>().CanMove = false;
            }
        }
    }

    private void setPlayerCanMove()
    {
        player.GetComponent<VRPlayerController>().CanMove = true;
    }

    public void createTiger()
    {
        Instantiate(tigerPrefab, GameObject.Find("KittyStandPoint").transform.position, Quaternion.identity); 
    }

    public void createSmoke()
    {
        Destroy(GameObject.Find("KittenNpc(Clone)"), 3.5f);
        Instantiate(smokePrefab, GameObject.Find("KittyStandPoint").transform.position, Quaternion.identity);
        Invoke("createTiger", 3.5f);
        Invoke("setPlayerCanMove", 4.5f);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatOwnerController : MonoBehaviour {

    public Transform standPoint;
    private GameObject player;
    public GameObject tigerPrefab;
    public GameObject smokePrefab;
    private GameManager gameManager;
    public GameObject dialogePrefab;
    private GameObject activeDialoge;
    public Transform dialogeSpawnPoint;
    private bool dialogeActive = false;
    private bool firstDialogeDone = false;
    private int dialogeStateCounter = 0;
    private string[] firstEncounterText;
    private string[] secondEncounterText;

    void Start () {
        player = GameObject.Find("Player");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();  
        firstEncounterText = new string[]{ "My Kittens... ! \n They are terrified in there. \n Why are you chasing them? ",
                              "This must be that cruel Ogre's doing again...",
                              "You need to help me deafet him! \n But first...",
                              "Acient Runes can be found in these forests. \n Find them and learn their secrets!",
                              "I marked the Runes for you.. \n Hurry up my kittens are in danger"};
        secondEncounterText = new string[] { "I see you learnt all the spells.", "Well done !",
                                             "Let's go find that Ogre, \n but first...",
                                             "Let my most loyal companion aid you in your battles"};
    }
	
	
	void Update () {
        transform.position = Vector3.MoveTowards(transform.position, standPoint.position, Time.deltaTime*1);
        transform.LookAt(player.transform);
        if (dialogeActive)
        {
            if ((Input.GetKeyDown(KeyCode.Space) || GvrController.ClickButtonDown || GvrController.TouchDown))
            {
                if (!firstDialogeDone)
                {
                    continoueFirstDialoge();
                }
                else
                {
                    continoueSecondDialoge();
                }
               
            }
        }
    }

    private void continoueFirstDialoge()
    {
        if (dialogeStateCounter < firstEncounterText.Length)
        {
            activeDialoge.GetComponent<DialogeController>().setDialogeText(firstEncounterText[dialogeStateCounter++]);
        }
        else
        {
            dialogeActive = false;
            firstDialogeDone = true;
            dialogeStateCounter = 0;
            player.GetComponent<VRPlayerController>().CanMove = true;
            DestroyImmediate(activeDialoge);
            gameManager.spawnRune(gameManager.currentRune++);
        }
    }

    private void continoueSecondDialoge()
    {
        if (dialogeStateCounter < secondEncounterText.Length)
        {
            activeDialoge.GetComponent<DialogeController>().setDialogeText(secondEncounterText[dialogeStateCounter++]);
        }
        else
        {
            dialogeActive = false;
            DestroyImmediate(activeDialoge);
            createSmoke();
            gameManager.reSpawnOgre();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!firstDialogeDone)
            {
                startDialoge();
                continoueFirstDialoge();
            }
            else if (gameManager.PracticeRunesDone)
            {
                startDialoge();
                continoueSecondDialoge();
            }
        }
    }

    private void startDialoge()
    {
        dialogeActive = true;
        player.GetComponent<VRPlayerController>().CanMove = false;
        player.GetComponent<VRPlayerController>().move = false;
        activeDialoge = Instantiate(dialogePrefab, dialogeSpawnPoint.position, Quaternion.identity);
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

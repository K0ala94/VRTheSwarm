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
    private bool secondDialogeDone = false;
    private bool thirdDialogeDone = false;
    private int dialogeStateCounter = 0;
    private string[] firstEncounterText;
    private string[] secondEncounterText;
    private string[] thirdEncounterText;

    void Start () {
        player = GameObject.Find("Player");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();  
        firstEncounterText = new string[]{ "My Kittens... ! \n They are terrified in there. \n Why are you chasing them? ",
                              "This must be that cruel Ogre's doing again...",
                              "You need to help me deafet him! \n But first...",
                              "Acient Runes can be found in these forests. \n Find them and learn their secrets!",
                              "I marked the Runes for you.. \n Hurry up my kittens are in danger"};
        secondEncounterText = new string[] { "I see you found all the Runes \n Well done !",
                                            "You can cast a spell by drawing a rune and swinging your wand forward",
                                            "Now go practice it!!"};
        thirdEncounterText = new string[] { "Great job ! \n You have mastered all the spells",
                                             "Let's go find that Ogre, \n but first...",
                                             "Let my most loyal companion aid you in your battles"};
    }
	
	
	void Update () {
        transform.position = Vector3.MoveTowards(transform.position, standPoint.position, Time.deltaTime*1);
        transform.LookAt(player.transform);
        if (dialogeActive)
        {
            if ((Input.GetKeyDown(KeyCode.Space) || GvrController.ClickButtonDown))
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
            finishDialoge();
            firstDialogeDone = true;
            gameManager.spawnRune();
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
            finishDialoge();
            secondDialogeDone = true;
            gameManager.spawnRune();
        }
    }

    private void continoueThirdDialoge()
    {
        if (dialogeStateCounter < thirdEncounterText.Length)
        {
            activeDialoge.GetComponent<DialogeController>().setDialogeText(thirdEncounterText[dialogeStateCounter++]);
        }
        else
        {
            finishDialoge();
            thirdDialogeDone = true;
            gameManager.reSpawnOgre();
            createSmoke();
        }
    }

    private void finishDialoge()
    {
        dialogeActive = false;
        dialogeStateCounter = 0;
        DestroyImmediate(activeDialoge);
        player.GetComponent<VRPlayerController>().CanMove = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerInteract"))
        {
            if (!firstDialogeDone)
            {
                startDialoge();
                continoueFirstDialoge();
            }
            else if (gameManager.LearnRunesDone && !secondDialogeDone)
            {
                startDialoge();
                continoueSecondDialoge();
            }
            else if (gameManager.PracticeRunesDone)
            {
                startDialoge();
                continoueThirdDialoge();
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

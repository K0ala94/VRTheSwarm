using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizzardController : MonoBehaviour {

    private GameObject player;
    public  GameObject dialogePrefab;
    public Transform dialogeSpawnPoint;
    private GameObject activeDialoge;
    private GameObject home;
    private GameManager gameManager;
    private bool questAccepted = false;
    private bool dialogeActive = false;
    private string[] firstEncounterText;
    private string[] secondEncounterText;
    private int dialogeStateCounter = 0;
    public float health = 100;

    private readonly string[] enDialoge1 = new string[] {"Greetings Wonderer! \n What a pleasent surprise, this can't be a coincidance... ",
                                             "I would be extremely grateful if you could help me out, \n and generous to say the least",
                                             "My delicious ugrhmmmmm... \n My dear kittens ran away from me \n They dwell in these forests now",
                                             "Could you catch at least one of them for me please? \n My caldron is ughh.. My life is empty without them",
                                             "Now go find them!"};

    private readonly string[] enDialoge2 = new string[] {"A TIGER !? \n I see you spoke to the wizzard",
                                            "Hahh you cant imagine how big of a mistake you made!",
                                            "and now PREPARE TO DIE !!!"};

    private readonly string[] hunDialoge1 = new string[] { "Köszöntelek Kalandor! \n Érkezésed nem lehet véletlen.. ",
                                            "Az én ízletes ughrmmm.. Az én kedves kis cicáim elszöktek a erdőbe.",
                                            "El tudnád nekem kapni legalább egyiküket? \n Az ebédem ugrhmm.. A világot jelentik nekem!",
                                            "Most menj! Keresd meg őket!" };

    private readonly string[] hunDialoge2 = new string[] {"Egy TIGRIS !? \n  Látom beszéltél a varázslóval..",
                                            "Hahh Bolond!! El sem tudod képzelni mekkora hibát követtél el",
                                            "Készülj a HALÁLRA !!!"};

    void Start () {
        player = GameObject.Find("Player");
        home = GameObject.Find("OgreDoor");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        firstEncounterText = hunDialoge1;
        secondEncounterText = hunDialoge2;
}
	
	
	void Update () {
        if(player == null)
        {
            player = GameObject.Find("Player");
        }

        if (gameManager.OgreAlive)
        {
            if (!questAccepted || gameManager.PlayerReturnedToOgre)
            {
                Vector3 startRot = transform.rotation.eulerAngles;
                transform.LookAt(player.transform);
                Vector3 rot = transform.rotation.eulerAngles;
                transform.rotation = Quaternion.Euler(new Vector3(startRot.x, rot.y, startRot.z));
            }
            else
            {
                transform.LookAt(home.transform);
                transform.position += transform.forward * Time.deltaTime * 0.9f;
            }

            if (dialogeActive && (Input.GetKeyDown(KeyCode.Space) || GvrController.ClickButtonDown))
            {
                if (!gameManager.PlayerReturnedToOgre)
                    continoueFirstDialoge();
                else
                    continoueSecondDialoge();
            }

            if (gameManager.Phase1)
            {
                //Do attacks here
                if (health < 50)
                {
                    gameManager.Phase1 = false;
                    gameManager.Phase2 = true;
                }
                
            }
            else if (gameManager.Phase2)
            {
                
            }

        }
        else
        {
            
            //StartCoroutine(die());
        }

	}

    private void OnTriggerEnter(Collider other)
    {
        if(player == null)
        {
            player = GameObject.Find("Player");
        }

        if (gameManager.OgreAlive && ( !gameManager.Phase1 && !gameManager.Phase2))
        {
            if (other.gameObject.CompareTag("PlayerInteract") && !questAccepted)
            {
                startDialoge();
                continoueFirstDialoge();
            }
            else if (other.gameObject.CompareTag("Player") && gameManager.PlayerReturnedToOgre)
            {
                if (!dialogeActive) {
                    startDialoge();
                    continoueSecondDialoge();
                }
                
            }
            else if (other.tag.Equals("WizzardsHome") && !gameManager.PlayerReturnedToOgre)
            {
                Destroy(gameObject, 0.1f);
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
            dialogeStateCounter = 0;
            player.GetComponent<VRPlayerController>().CanMove = true;
            DestroyImmediate(activeDialoge);
            AcceptQuest();
        }

    }

    private void continoueSecondDialoge()
    {
        if(dialogeStateCounter < secondEncounterText.Length)
        {
            activeDialoge.GetComponent<DialogeController>().setDialogeText(secondEncounterText[dialogeStateCounter++]);
        }
        else
        {
            dialogeActive = false;
            dialogeStateCounter = 0;
            DestroyImmediate(activeDialoge);
            StartCoroutine(enrage());
            gameManager.Phase1 = true;
            Invoke("startSpawningRunes", 4.0f);
            player.GetComponent<VRPlayerDash>().fightStandPos = player.transform.position;
            player.GetComponent<VRPlayerDash>().initialFightPos = player.transform.position;
            GetComponent<BarrelSpawner>().startBarrelThrowing();
        }

    }

    private void startSpawningRunes()
    {
        gameManager.spawnRune();
    }


    private void startDialoge()
    {
            activeDialoge = (GameObject)Instantiate(dialogePrefab, dialogeSpawnPoint.position, Quaternion.identity);
            dialogeActive = true;
            player.GetComponent<VRPlayerController>().CanMove = false;
            player.GetComponent<VRPlayerController>().move = false;
    }

    public void AcceptQuest()
    {
        DestroyImmediate(activeDialoge);
        questAccepted = true;
        dialogeActive = false;
        player.GetComponent<VRPlayerController>().CanMove = true;
        gameManager.spawnKittens();
    }

    private IEnumerator enrage()
    {
        for(int i= 1; i < 200; i++)
        {
            transform.localScale *= 1.005f;
            yield return null;
        }
       
    }

    public IEnumerator die()
    {
        for (int i = 1; i < 90; i++)
        {
            transform.Rotate(new Vector3(-1, 0, 0));
            yield return null;
        }
    }
}

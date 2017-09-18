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

    void Start () {
        player = GameObject.Find("Player");
        home = GameObject.Find("OgreDoor");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        firstEncounterText = new string[] {"Greetings Wonderer! \n What a pleasent surprise, this can't be a coincidance... ",
                             "I would be extremely greatful if you could help me out, \n and generous to say the least",
                             "My delicious ugrhmmmmm... \n My dear kittens ran away from me \n They dwell in these forests now",
                             "Could you catch at least one of them for me please? \n My caldron is ughh.. My life is empty without them",
                             "Now go find them!"};
        secondEncounterText = new string[] {"A TIGER !? \n I see you spoke to the wizzard",
                                            "Hahh you cant imagine how big of a mistake you made!",
                                            "and now PREPARE TO DIE !!!"};
}
	
	
	void Update () {
        if (!questAccepted || gameManager.PlayerReturnedToOgre)
        {
            transform.LookAt(player.transform);
        }
        else
        {
            transform.LookAt(home.transform);
            transform.position += transform.forward * Time.deltaTime * 0.9f;
        }

        if(dialogeActive && (Input.GetKeyDown(KeyCode.Space) || GvrController.ClickButtonDown)){
            if (!questAccepted)
                continoueFirstDialoge();
            else
                continoueSecondDialoge();
        }

	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerInteract") && !questAccepted)
        {
            startDialoge();
            continoueFirstDialoge();
        }
        else if(other.gameObject.CompareTag("Player") && gameManager.PlayerReturnedToOgre)
        {
            startDialoge();
            continoueSecondDialoge();
        }
        else if (other.tag.Equals("WizzardsHome") && !gameManager.PlayerReturnedToOgre)
        {
            Destroy(gameObject, 0.1f);
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
            DestroyImmediate(activeDialoge);
            StartCoroutine(enrage());
        }

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
}

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
    private Animator catOwnerAnimator;

    private string[] dialogeHUn1 = new string[] {"A cicáim.. ! \nHalálra vannak rettenve odabent. \nMegtudhatnám miért kergeted őket?",
                                            "Fogadjunk, hogy egint annak az álnok Ogrénak a keze van ebben \n CICÁKAT eszik!!", 
                                            "Segítened kell legyőzni őt! \n De még nem állsz készen rá, tanulnod kell!",
                                            "Ősi erővel bíró rúnák találhatók az erdőben \n Találd meg mind a négyet és rajzold végig őket",
                                            "Megjelöltem a rúnákat neked egy kis varázylattal \n Ha nem tudod merre tovább csak tekints fel az égre",
                                            "Siess a kiscicáim veszélyben vannak"};
    private string[] dialogeHUn2 = new string[] {"Látom megtaláltad az összes rúnát \n Szép munka!",
                                                "Ezen rúnák segítségével hatásos varázslatot szórhatsz az Ogréra \n Csak rajzold végig őket és a megjelenő varázsgömböt dobómozdulattal el tudod lőni",
                                                "Most menj gyakorolj! \n Térj vissza hozzám ha kész vagy"};
    private string[] dialogeHUn3 = new string[] {"Készen állsz a harcra! \n Elsajátítottad a varázslatokat",
                                                 "Irány az ádáz Ogre háza \n Ne felejtsd el, a támadásai elől el tudsz ugrani a a controller elején található touchpad-en jobbra, vagy balra swipe-olsz",
                                                 "Mielőtt még elindulnánk, \n hadd kisérjen el téged egyik leghűségesebb társam"};
    private string[] dialogeEn1 = new string[] { "My Kittens... ! \n They are terrified in there. \n Why are you chasing them? ",
                                            "This must be that cruel Ogre's doing again... \n HE EATS KITTENS !!",
                                            "You need to help me defeat him! \n But you need to prepare for the fight first!",
                                            "Acient Runes can be found in these forests. \n Find all four of them and learn their secrets!",
                                            "I marked the Runes for you.. \n Just look up in the sky if you are lost. \n Hurry up my kittens are in danger" };
    private string[] dialogeEn2 = new string[] {"I see you found all the Runes \n Well done !",
                                            "You can cast a spell by drawing a rune and swinging your wand forward when the elemnets of nature appear",
                                            "Now go practice it!! \n Come back to me when you are done!" };
    private string[] dialogeEn3 = new string[] { "Great job ! \n You have mastered all the spells",
                                             "Let's go find that Ogre! \n Remember, you can dodge his attacks by swiping on the touchpad left and right",
                                             "In addition, \nLet my most loyal companion aid you in your battles"};


    void Start () {
        player = GameObject.Find("Player");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        catOwnerAnimator = GetComponent<Animator>();
        firstEncounterText = dialogeHUn1;
        secondEncounterText = dialogeHUn2;
        thirdEncounterText = dialogeHUn3;

    }
	
	
	void Update () {
        transform.position = Vector3.MoveTowards(transform.position, standPoint.position, Time.deltaTime*1);
        //Csak az y tengely korul forogjon
        Vector3 startRot = transform.rotation.eulerAngles;
        transform.LookAt(player.transform);
        Vector3 rot = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(new Vector3(startRot.x, rot.y, startRot.z));

        if(transform.position.x == standPoint.position.x && transform.position.y == standPoint.position.y)
        {
            catOwnerAnimator.SetBool("Arrived", true);
        }

        if (dialogeActive)
        {
            if ((Input.GetKeyDown(KeyCode.Space) || GvrController.ClickButtonDown))
            {
                if (!firstDialogeDone)
                {
                    continoueFirstDialoge();
                }
                else if(!secondDialogeDone)
                {
                    continoueSecondDialoge();
                }
                else
                {
                    continoueThirdDialoge();
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
            gameManager.reSpawnOgre(true);
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
        if (other.gameObject.CompareTag("PlayerInteract") && !dialogeActive)
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
            else if (gameManager.PracticeRunesDone && !thirdDialogeDone)
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

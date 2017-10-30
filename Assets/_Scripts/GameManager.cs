using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public int currentRune = 0;

    public GameObject[] runePrefabs;
    public Transform[] spawnPoints;
    public GameObject kittenSpawnerCenter;
    public GameObject ogrePrefab;
    private GameObject menu;
    private GameObject restartButton;
    private GameObject pauseRestartButton;
    public GameObject tigerPrefab;
    public Transform ogreSpawnPoint;
    public GameObject markerPrefab;
    public GameObject playerPrefab;
    public Transform runePracticeSpawnPoint;
    public Transform runeOgreSpawnPoint;
    public Transform respawnPoint;
    public Transform playerStartSpawn;
    private AudioManager audioManager;
    private bool paused = false;

    public bool godMode;
    public bool PracticeRunesDone { get; set; }
    public bool LearnRunesDone { get; set; }
    public bool PlayerReturnedToOgre { get; set; }
    public bool OgreAlive { get; set; }
    public bool Phase1 { get; set; }
    public bool Phase2 { get; set; }

    private void Start()
    {
        GameStatistics.resetStats();
        
        audioManager = FindObjectOfType<AudioManager>();

        menu = GameObject.Find("StartPhaseDialoge");
        restartButton = GameObject.Find("RestartButton");
        pauseRestartButton = GameObject.Find("PauseRestartButton");

        menu.SetActive(false);
        restartButton.SetActive(false);
        pauseRestartButton.SetActive(false);

        fadeIn();
        PracticeRunesDone = false;
        LearnRunesDone = false;
        PlayerReturnedToOgre = false;
        godMode = false;
        OgreAlive = true;
        Phase1 = false;
        Phase2 = false;
    }

    private void Update()
    {
        if (GvrControllerInput.AppButtonDown || Input.GetKeyDown(KeyCode.P))
        {
            pauseGame();
        }
    }

    public void startGame()
    {
        audioManager.playSound("click");
        GameObject.Find("Player").GetComponent<VRPlayerController>().CanMove = true;
        menu.SetActive(false);
    }


    public void spawnRune()
    {
        Destroy(GameObject.Find("Sparkl(Clone)"), 0.1f);
        
        if (!LearnRunesDone)
        {
            if (currentRune < runePrefabs.Length && currentRune < spawnPoints.Length)
            {
                Instantiate(runePrefabs[currentRune], spawnPoints[currentRune].position, Quaternion.identity);
                currentRune++;
            }
            else
            {
                currentRune = 0;
                LearnRunesDone = true;
                Instantiate(markerPrefab, GameObject.Find("DialogeSpawn").transform.position, Quaternion.Euler(-90, 0, 0));
            }
        }
        else if (!PracticeRunesDone)
        {
            if (currentRune < runePrefabs.Length)
            {
                Vector3 runePos = runePracticeSpawnPoint.position + (Random.insideUnitSphere * 6);
                runePos.y = 1.5f;
                Instantiate(runePrefabs[currentRune], runePos, Quaternion.identity);
                currentRune++;
            }
            else
            {
                PracticeRunesDone = true;
            }
        }
        else
        {
            Vector3 runePos = runeOgreSpawnPoint.position + (Random.insideUnitSphere * 1.5f);
            int runeIndex = Random.Range(0, 4);
            GameObject rune = Instantiate(runePrefabs[runeIndex], runePos, Quaternion.identity);
            //ne legyen marker folotte lathato
            ParticleSystem.EmissionModule emit = rune.GetComponentInChildren<ParticleSystem>().emission;
            emit.enabled = false;
        }
    }

    private void spawnTiger(bool shouldRespawnTiger)
    {
        DestroyImmediate(GameObject.Find("Tiger"));
        if(shouldRespawnTiger)
            Instantiate(tigerPrefab, GameObject.Find("TigerRespawnPoint").transform.position, Quaternion.identity).name = "Tiger";
    }

    public void spawnKittens()
    {
        kittenSpawnerCenter.GetComponent<KittenSpawner>().spawnKittens();
    }

    public void dealyedDisplayRestartButton()
    {
        Invoke("displayRestartButton", 3.0f);
    }

    private void displayRestartButton()
    {
        restartButton.SetActive(true);
    }

    public void reSpawnOgre(bool returnToOgre)
    {
        DestroyImmediate(GameObject.Find("Ogre"));
        Instantiate(ogrePrefab, ogreSpawnPoint.position, Quaternion.identity).name = "Ogre";
        PlayerReturnedToOgre = returnToOgre;
    }

    public void respawnPlayer()
    {
        Phase1 = false;
        Phase2 = false;
        fadeOut();
        Invoke("resetGameStateToBeforeFight", 4.0f);
    }

    private void resetGameStateToBeforeFight()
    {
        DestroyImmediate(GameObject.FindGameObjectWithTag("Rune"));
        reSpawnOgre(true);
        DestroyImmediate(GameObject.Find("Player"));
        Instantiate(playerPrefab, respawnPoint.position, Quaternion.identity).name = "Player";

        pauseRestartButton.GetComponent<DialogeController>().player = GameObject.Find("Player");
        spawnTiger(true);

        GameObject.Find("Player").GetComponent<Transform>().Rotate(0,50,0);
        
        fadeIn();
    }

    public void jumpToOgreFight()
    {
        audioManager.playSound("click");

        menu.SetActive(false);

        PracticeRunesDone = true;
        LearnRunesDone = true;
        PlayerReturnedToOgre = true;
        godMode = true;

        respawnPlayer();
    }

    public void continueToTheMenu()
    {
        audioManager.playSound("click");
        DestroyImmediate(GameObject.Find("Manual").gameObject);
        menu.SetActive(true);
    }

    public void restartGameOnClick()
    {
        audioManager.playSound("click");

        if(paused)
            pauseGame();
        fadeOut();
        Invoke("restartGame", 4.0f);
    }

    private void restartGame()
    {
        GameStatistics.createEndgameStatistics();
        GameStatistics.resetStats();

        reSpawnOgre(false);
        spawnTiger(false);
        DestroyImmediate(GameObject.Find("CatOwner"));
        DestroyImmediate(GameObject.FindGameObjectWithTag("Rune"));
        DestroyImmediate(GameObject.Find("DialogeWorldSapce(Clone)"));

        menu.SetActive(true);
        restartButton.SetActive(false);
        
        GameObject player = GameObject.Find("Player");
        player.GetComponent<VRPlayerController>().CanMove = false;
        player.transform.position = playerStartSpawn.position;

        PracticeRunesDone = false;
        LearnRunesDone = false;
        PlayerReturnedToOgre = false;
        OgreAlive = true;
        Phase1 = false;
        Phase2 = false;

        fadeIn();
    }

    public void pauseGame()
    {
        if (paused)
        {
            Time.timeScale = 1;
            pauseRestartButton.SetActive(false);
            paused = false;
        }
        else
        {
            Time.timeScale = 0;
            pauseRestartButton.transform.position = GameObject.Find("RestartButtonLocation").transform.position;
            pauseRestartButton.GetComponent<DialogeController>().redirect(); 
            pauseRestartButton.SetActive(true);
            paused = true;
        }
    }


    private void fadeIn()
    {
        GameObject.Find("FadePanel").GetComponent<Image>().CrossFadeAlpha(0f, 2.0f, true);
    }

    private void fadeOut()
    {
        GameObject.Find("FadePanel").GetComponent<Image>().CrossFadeAlpha(1.0f, 4.0f, true);
    }

}

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
    public Transform ogreSpawnPoint;
    public GameObject markerPrefab;
    public GameObject playerPrefab;
    public Transform runePracticeSpawnPoint;
    public Transform runeOgreSpawnPoint;
    public Transform respawnPoint;


    public bool PracticeRunesDone { get; set; }
    public bool LearnRunesDone { get; set; }
    public bool PlayerReturnedToOgre { get; set; }
    public bool OgreAlive { get; set; }
    public bool Phase1 { get; set; }
    public bool Phase2 { get; set; }

    private void Start()
    {
        fadeIn();
        PracticeRunesDone = false;
        LearnRunesDone = false;
        PlayerReturnedToOgre = false;
        OgreAlive = true;
        Phase1 = false;
        Phase2 = false;
    }

    public void startGame()
    {
        GameObject.Find("Player").GetComponent<VRPlayerController>().CanMove = true;
        Destroy(GameObject.Find("StartPhaseDialoge"), 0.1f);
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

    public void spawnKittens()
    {
        kittenSpawnerCenter.GetComponent<KittenSpawner>().spawnKittens();
    }

    public void reSpawnOgre()
    {
        Instantiate(ogrePrefab, ogreSpawnPoint.position, Quaternion.identity).name = "Ogre";
        PlayerReturnedToOgre = true;
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
        DestroyImmediate(GameObject.Find("Ogre"));
        DestroyImmediate(GameObject.FindGameObjectWithTag("Rune"));
        reSpawnOgre();
        DestroyImmediate(GameObject.Find("Player"));
        Instantiate(playerPrefab, respawnPoint.position, Quaternion.identity).name = "Player";
        
        GameObject.Find("Player").GetComponent<Transform>().Rotate(0,50,0);
        
        fadeIn();
    }

    public void jumpToOgreFight()
    {
        Destroy(GameObject.Find("StartPhaseDialoge"),0.1f);

        PracticeRunesDone = true;
        LearnRunesDone = true;
        PlayerReturnedToOgre = true;

        respawnPlayer();

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

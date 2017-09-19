using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public int currentRune = 0;

    public GameObject[] runePrefabs;
    public Transform[] spawnPoints;
    public GameObject kittenSpawnerCenter;
    public GameObject ogrePrefab;
    public Transform ogreSpawnPoint;
    public GameObject markerPrefab;
    public Transform runePracticeSpawnPoint;

    public bool LearnMode { get; set; }
    public bool PracticeMode { get; set; }
    public bool PracticeRunesDone { get; set; }
    public bool LearnRunesDone { get; set; }
    public bool PlayerReturnedToOgre { get; set; }

    private void Start()
    {
        LearnMode = true;
        PracticeMode = false;
        PracticeRunesDone = false;
        LearnRunesDone = true;
        LearnMode = true;
        PlayerReturnedToOgre = false;
    }


    public void spawnRune()
    {
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
                Instantiate(runePrefabs[currentRune], runePracticeSpawnPoint.position, Quaternion.identity);
                currentRune++;
            }
            else
            {
                PracticeRunesDone = true;
                reSpawnOgre();
            }
        }
        else
        {

        }
    }

    public void spawnKittens()
    {
        kittenSpawnerCenter.GetComponent<KittenSpawner>().spawnKittens();
    }

    public void reSpawnOgre()
    {
        Instantiate(ogrePrefab, ogreSpawnPoint.position, Quaternion.identity);
        PlayerReturnedToOgre = true;
    }

}

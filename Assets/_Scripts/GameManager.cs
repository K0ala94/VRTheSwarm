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

    public bool PracticeMode { get; set; }
    public bool PracticeRunesDone { get; set; }
    public bool PlayerReturnedToOgre { get; set; }

    private void Start()
    {
        PracticeRunesDone = false;
        PracticeMode = true;
        PlayerReturnedToOgre = false;
    }


    public void spawnRune(int runeIndex)
    {
        if (runeIndex < runePrefabs.Length && runeIndex < spawnPoints.Length)
        {
            Instantiate(runePrefabs[runeIndex], spawnPoints[runeIndex].position, Quaternion.identity);
        }
        else
        {
            PracticeRunesDone = true;
            Instantiate(markerPrefab, GameObject.Find("DialogeSpawn").transform.position, Quaternion.EulerAngles(-90,0,0));
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

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
    public Transform runeOgreSpawnPoint;

    public bool PracticeRunesDone { get; set; }
    public bool LearnRunesDone { get; set; }
    public bool PlayerReturnedToOgre { get; set; }
    public bool OgreAlive { get; set; }
    public bool Phase1 { get; set; }
    public bool Phase2 { get; set; }

    private void Start()
    {
        PracticeRunesDone = true;
        LearnRunesDone = true;
        PlayerReturnedToOgre = true;
        OgreAlive = true;
        Phase1 = false;
        Phase2 = false;
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
            Instantiate(runePrefabs[runeIndex], runePos, Quaternion.identity);
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

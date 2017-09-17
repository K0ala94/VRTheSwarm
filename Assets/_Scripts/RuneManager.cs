using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneManager : MonoBehaviour {

    public int currentRune = 0;

    public GameObject[] runePrefabs;
    public Transform[] spawnPoints;

    public void spawnRune(int runeIndex)
    {
        if (runeIndex < runePrefabs.Length && runeIndex < spawnPoints.Length)
        {
            Instantiate(runePrefabs[runeIndex], spawnPoints[runeIndex].position, Quaternion.identity);
        }
    }

}

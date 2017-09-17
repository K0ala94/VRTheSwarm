using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KittenSpawner : MonoBehaviour {

    public GameObject kittenPrefab;
    public int numberOfKittens;

    public void spawnKittens()
    {
        for(int i=0; i < numberOfKittens; i++)
        {
            Vector3 dir = Random.insideUnitSphere* 7;
            Vector3 spawnPoint = new Vector3(transform.position.x +dir.x,0,transform.position.z + dir.z);
            Instantiate(kittenPrefab,spawnPoint, Quaternion.identity);
        }
    }

	void Start () {
		
	}
	
	
	void Update () {
		
	}
}

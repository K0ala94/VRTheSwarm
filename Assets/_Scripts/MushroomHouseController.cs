using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomHouseController : MonoBehaviour {

    public GameObject kittenOwnerPrefab;
    public GameObject nest;
    public GameObject kittenNpcPrefab;
    private int kittenCount = 0;
    private KittenSpawner spawner;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Kitten"))
        {
            if (++kittenCount == spawner.numberOfKittens)
            {
                Instantiate(kittenOwnerPrefab, nest.transform.position, Quaternion.identity);
                Instantiate(kittenNpcPrefab, nest.transform.position, Quaternion.identity);
            }
        }
    }

    void Start () {
        spawner = GameObject.Find("KittenSpawnCenter").GetComponent<KittenSpawner>();
	}
	
	
	void Update () {
		
	}
}

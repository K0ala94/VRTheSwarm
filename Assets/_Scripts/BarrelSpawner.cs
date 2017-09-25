using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelSpawner : MonoBehaviour {

    public GameObject barrelPrefab;
    public Transform barrelSpawn;
    private GameManager gameManager;
    private float spawnTime;

	void Start () {
        spawnTime = 10.0f;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
    }

    void Update () {

        if (Input.GetKeyDown(KeyCode.B))
        {
            Instantiate(barrelPrefab, barrelSpawn.position, Quaternion.identity);
        }
    }

    public void startBarrelThrowing()
    {
        StartCoroutine(spawnBarrels());
    }

    private IEnumerator spawnBarrels()
    {
        yield return new WaitForSeconds(5.0f);
        while (gameManager.OgreAlive && gameManager.Phase1)
        {
            Instantiate(barrelPrefab, barrelSpawn.position, Quaternion.identity);
            yield return new WaitForSeconds(spawnTime);
        }
    }
}

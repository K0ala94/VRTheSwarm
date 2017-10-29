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

        int meditation = AdaptEDConnector.Meditation;
        spawnTime = 40000 / (meditation * meditation);
    }

    public void startBarrelThrowing()
    {
        StartCoroutine(spawnBarrels());
    }

    private IEnumerator spawnBarrels()
    {
        yield return new WaitForSeconds(7.0f);
        while (gameManager.OgreAlive && ( gameManager.Phase1 || gameManager.Phase2))
        {
            GameObject barrel = Instantiate(barrelPrefab, barrelSpawn.position, Quaternion.identity);
            Destroy(barrel, 4.0f);

            FindObjectOfType<AudioManager>().playSound("barrelthrow");

            yield return new WaitForSeconds(spawnTime);
        }
    }
}

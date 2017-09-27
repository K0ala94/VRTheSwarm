using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    private int health = 100;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void decreaseHealth(int damage)
    {
        health -= damage;
        Debug.Log(health);
        if(health <= 0)
        {
            Debug.Log(" YOU DIED !");
            StartCoroutine(die());
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<VRPlayerController>().CanMove = false;
            gameManager.respawnPlayer();
        }
    }

    public IEnumerator die()
    {
        for (int i = 1; i < 90; i++)
        {
            transform.Rotate(new Vector3(-1, 0, 0));
            yield return null;
        }
    }

}

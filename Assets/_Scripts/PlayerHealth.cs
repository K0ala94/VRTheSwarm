using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    private int health = 100;

    public void decreaseHealth(int damage)
    {
        health -= damage;
        Debug.Log(health);
        if(health <= 0)
        {
            Debug.Log(" YOU DIED !");
        }
    }
	
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    private int health = 100;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
    }

    public void decreaseHealth(int damage)
    {
        Image damageFilter = GameObject.Find("DamageFilterImg").GetComponent<Image>();
        Color dmgFilterColor = damageFilter.color;
        dmgFilterColor.a = 0.5f;
        damageFilter.color = dmgFilterColor;
        damageFilter.CrossFadeAlpha(1.0f, 0.01f, true);

        if (!gameManager.godMode)
        {
            health -= damage;
            Debug.Log(health);
        }
            

        if (health <= 0)
        {
            Debug.Log(" YOU DIED !");
            StartCoroutine(die());
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<VRPlayerController>().CanMove = false;
            gameManager.respawnPlayer();
        }
        else
        {
            damageFilter.CrossFadeAlpha(0f, 1.5f, true);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireBallController : MonoBehaviour {

    private GameObject DDController;
    private GameManager gameManager;
    public GameObject damageDisplayPrefab;
    public bool shooting = false;
    public float damage = 0;
    private float swingStartTime;
    private bool swinging = false;

    void Start () {
        DDController = GameObject.Find("GvrControllerPointer");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
	
	void Update () {
        if(!shooting)
            transform.position = DDController.transform.position + DDController.transform.forward * 1; 
	}

    private void FixedUpdate()
    {
       //controller szogsebessege radianban
        if (!shooting && GvrControllerInput.Gyro.x > 3.14f)
        {
            if (!swinging)
            {
                swingStartTime = Time.time;
                swinging = true;
            }
            else if (Time.time - swingStartTime > 0.15f)
            {
                shoot();
            }
        }
        else
        {
            swinging = false;
        }


    }

    public void shoot()
    {
        //Kamera irányába repüljön és egy kicsit felfel
        shooting = true;
        Transform camera = GameObject.Find("VrCamera").transform;
        Quaternion origRot = camera.rotation;
        camera.Rotate(new Vector3(-10f, 0, 0));
        GetComponent<Rigidbody>().velocity = camera.forward * GvrControllerInput.Gyro.x* 2;
        camera.rotation = origRot;
        Destroy(gameObject, 4);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wizzard"))
        {
            other.GetComponent<WizzardController>().health -= damage;

            Instantiate(damageDisplayPrefab, transform.position, Quaternion.identity);
            Text dmgText = GameObject.Find("DamageText").GetComponent<Text>();
            dmgText.text ="-"+ damage;

            Debug.Log(other.GetComponent<WizzardController>().health);
            if(other.GetComponent<WizzardController>().health <= 0)
            {
                gameManager.Phase2 = false;
                gameManager.OgreAlive = false;
                GameObject.Find("Player").GetComponent<VRPlayerController>().CanMove = true;
                StartCoroutine(GameObject.Find("Ogre").GetComponent<WizzardController>().die());
                gameManager.dealyedDisplayRestartButton();
            }
        }
        
    }
}

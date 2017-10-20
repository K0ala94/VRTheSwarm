using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRPlayerController : MonoBehaviour {

    public Transform vrCamera;
	public float toggleMoveAngle = 30f;
    private float playerSpeed;
    public bool move = false;
    private CharacterController cController;
    public GameObject fireBallPrefab;
    private GameManager gameManager;
    private AudioManager audioManager;
   
    

    public bool CanMove { private get; set; }

    void Start () {
        cController = GetComponent<CharacterController>();
        CanMove = false;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        audioManager = FindObjectOfType<AudioManager>();

    }
	
	
	void Update () {
        //mikor kezdodjon a mozgas
        //if (GvrControllerInput. IsTouching)
        //{
            if (vrCamera.eulerAngles.x > 20 && vrCamera.eulerAngles.x < 50)
            {
                move = true;
                playerSpeed = 2;
            }
            if (move && (vrCamera.eulerAngles.x > 15 && vrCamera.eulerAngles.x < 350))
            {
                playerSpeed = 3.2f;
            }
            else if (move)
            {
                playerSpeed = 2;
            }
            //felfele nezesnel abbamaradjon és akkor is ha egy bizonyos szognel lejjebb nez
            if ((move && vrCamera.eulerAngles.x < 350 && vrCamera.eulerAngles.x > 180) ||
               (move && vrCamera.eulerAngles.x > 50 && vrCamera.eulerAngles.x < 90))
            {
                move = false;
            }

            if (move && CanMove)
            {
                transform.position += new Vector3(vrCamera.forward.x, 0, vrCamera.forward.z) * playerSpeed * Time.deltaTime;
                // Vector3 forward = vrCamera.TransformDirection(Vector3.forward);
                // cController.SimpleMove(forward * playerSpeed);
            }
            manageWalkSound();
        //}

        if (GvrControllerInput.TouchUp)
        {
           //move = false;
        }

        if (GvrControllerInput.AppButtonDown)
        {

            
        }
	}

    private void manageWalkSound()
    {
        if(!audioManager.isPlaying("walkSound") && move && CanMove)
            audioManager.playSound("walkSound");
        else if(audioManager.isPlaying("walkSound") && (!move || !CanMove))
            audioManager.stopSound("walkSound");
    }

    private void FixedUpdate()
    {
        if (GvrControllerInput.AppButtonDown)
        {
           //GetComponent<Rigidbody>().AddForce(transform.up * 200f);
        }
    }
}

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
    private Vector2 initialTouchPos = new Vector2(0, 0);
    private bool canMoveLeftInFight = true;
    private bool canMoveRightInFight = true;
    public Vector3 fightStandPos;

    public bool CanMove { private get; set; }

    void Start () {
        cController = GetComponent<CharacterController>();
        CanMove = true;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
	
	void Update () {
        //mikor kezdodjon a mozgas
        if (GvrControllerInput. IsTouching)
        {
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
        }

        //Harc kozbeni dashelgetes
        if (GvrControllerInput.TouchDown)
        {
            initialTouchPos = GvrControllerInput.TouchPos;
        }
        if (GvrControllerInput.TouchUp)
        {
            move = false;
            float deltaPos = initialTouchPos.x - GvrControllerInput.TouchPos.x;
            if(deltaPos > 0.3)
            {
                if (canMoveLeftInFight)
                {
                    fightStandPos = transform.position - transform.right * 2;
                    canMoveLeftInFight = false;
                    canMoveRightInFight = true;
                }
            }
            else if ( deltaPos < -0.3)
            {
                if (canMoveRightInFight)
                {
                    fightStandPos = transform.position + transform.right * 2;
                    canMoveLeftInFight = true;
                    canMoveRightInFight = false;
                }
            }
        }

        if (gameManager.Phase1 || gameManager.Phase2)
        {
            transform.position = Vector3.Lerp(transform.position, fightStandPos, 0.15f);
        }

        if (GvrControllerInput.AppButtonDown)
        {
            GameObject controller = GameObject.Find("GvrControllerPointer");
            Vector3 pos = controller.transform.position + controller.transform.forward * 1;

            GameObject fireBall = Instantiate(fireBallPrefab, pos, Quaternion.identity);   
        }
	}

    private void FixedUpdate()
    {
        if (GvrControllerInput.AppButtonDown)
        {
           //GetComponent<Rigidbody>().AddForce(transform.up * 200f);
        }
    }
}

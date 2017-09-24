using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRPlayerDash : MonoBehaviour {

    public Vector3 fightStandPos;
    private GameManager gameManager;
    private Vector2 initialTouchPos = new Vector2(0, 0);
    private bool canMoveLeftInFight = true;
    private bool canMoveRightInFight = true;
    private GameObject vrCamera;

    void Start () {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        vrCamera = GameObject.Find("VrCamera");
    }
	
	
	void Update () {

        if (GvrControllerInput.TouchDown)
        {
            initialTouchPos = GvrControllerInput.TouchPos;
        }
        if (GvrControllerInput.TouchUp)
        {
            float deltaPos = initialTouchPos.x - GvrControllerInput.TouchPos.x;
            if (deltaPos > 0.3)
            {
                if (canMoveLeftInFight)
                {
                    fightStandPos = transform.position - vrCamera.transform.right * 3;
                    canMoveLeftInFight = false;
                    canMoveRightInFight = true;
                }
            }
            else if (deltaPos < -0.3)
            {
                if (canMoveRightInFight)
                {
                    fightStandPos = transform.position + vrCamera.transform.right * 3;
                    canMoveLeftInFight = true;
                    canMoveRightInFight = false;
                }
            }
        }

        if (gameManager.Phase1 || gameManager.Phase2)
        {
            transform.position = Vector3.Lerp(transform.position, fightStandPos, 0.15f);
        }
    }
}

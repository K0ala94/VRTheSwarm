using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRPlayerDash : MonoBehaviour {

    public Vector3 fightStandPos;
    public Vector3 initialFightPos;
    private GameManager gameManager;
    private Vector2 initialTouchPos = new Vector2(0, 0);
    private bool canMoveLeftInFight = true;
    private bool canMoveRightInFight = true;
    private GameObject vrCamera;
    private DashPos dashPos = DashPos.Mid;

    private enum DashPos { Left,Mid,Right};

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
                if (dashPos == DashPos.Mid)
                {
                    fightStandPos = transform.position - vrCamera.transform.right * 3;
                    dashPos--;
                }
                else if(dashPos == DashPos.Right)
                {
                    fightStandPos = initialFightPos;
                    dashPos--;
                }
            }
            else if (deltaPos < -0.3)
            {
                if (dashPos == DashPos.Mid)
                {
                    fightStandPos = transform.position + vrCamera.transform.right * 3;
                    dashPos++;
                }
                else if(dashPos == DashPos.Left)
                {
                    fightStandPos = initialFightPos;
                    dashPos++;
                }
            }
        }

        if (gameManager.Phase1 || gameManager.Phase2)
        {
           transform.position = Vector3.Lerp(transform.position, fightStandPos, 0.15f);
        }
    }
}

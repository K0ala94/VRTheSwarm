using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSun : MonoBehaviour {

	public float sunMoveSpeed;
    public bool doubleSpeed = false;

	void Start () {}



	void Update () {
        if (!doubleSpeed && transform.eulerAngles.z > 145 && transform.eulerAngles.z < 340)
        {
            toggleSpeedChange();
            doubleSpeed = true;
            
        }
        else if(doubleSpeed && transform.eulerAngles.z > 333 )
        {
            toggleSpeedChange();
            doubleSpeed = false;
            
        }
            
        transform.Rotate(0, 0, 1 * sunMoveSpeed);

	}

    private void toggleSpeedChange()
    {
        if(doubleSpeed)
            sunMoveSpeed /= 4;
        else
            sunMoveSpeed *= 4;
    }
}

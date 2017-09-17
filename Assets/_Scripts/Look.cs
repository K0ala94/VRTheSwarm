using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviour {

	
	void Start () {
		
	}
	
	
	void Update () {
        float rotateVertically = Input.GetAxis("Mouse Y");

       //transform.Rotate(-rotateVertically * 2f, 0, 0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneStartMarker : MonoBehaviour {

    private bool backwards = false;
    public Transform forth;
    public Transform back;


	void Start () {
       
    }


    void Update () {

        

       
        Debug.Log(Mathf.Abs(transform.position.y - forth.position.y));

        if (Mathf.Abs( transform.position.y - forth.position.y)  < 0.1f)
        {
            backwards = true;
        }
        else if(Mathf.Abs(transform.position.y - back.position.y) < 0.1f)
        {
            backwards = false ;
        }

        if (backwards)
        {
            transform.position = Vector3.Lerp(transform.position, back.position, 0.02f);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, forth.position, 0.02f);
        }
    }
}

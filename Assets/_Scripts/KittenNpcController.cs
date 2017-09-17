using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KittenNpcController : MonoBehaviour {

    private Animator kittenAnim;
    public Transform standPoint;

	void Start () {
        kittenAnim = GetComponent<Animator>();
        kittenAnim.SetBool("Scared", true);
        kittenAnim.SetBool("Fleeing", true);
	}
	
	
	void Update () {
        transform.position = Vector3.MoveTowards(transform.position, standPoint.position, Time.deltaTime * 1);
        if (transform.position.Equals(standPoint.position))
        {
            kittenAnim.SetBool("Scared", false);
            kittenAnim.SetBool("Fleeing", false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KittenController : MonoBehaviour {

    public float rockSpeed;
    public float fasterRockSpeed;
    public float fastestRockSpeed;
    public float avoidRadius;
    private float speed;
    private GameObject nest;
    GameObject player;
    private bool scared = false;
    private bool goingAroundSomething = false;
    private bool fleeing = false;
    private Animator kittenAnim;
    private GameObject obstacle;
    private bool canMove = true;

    void Start () {
        player = GameObject.Find("Player");
        nest = GameObject.Find("Nest");
        kittenAnim = GetComponent<Animator>();
        kittenAnim.SetBool("Scared", false);
        kittenAnim.SetBool("Fleeing", false);
        speed = rockSpeed;
        transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!scared)
            {
                Rigidbody rb = GetComponent<Rigidbody>();
                rb.AddForce(transform.up * 150);
            }
            kittenAnim.SetBool("Scared", true);
            scared = true;
        }
        else if (other.gameObject.CompareTag("Nest"))
        {
            Destroy(gameObject,0.01f);
        }
        else if (other.gameObject.CompareTag("Obstacle"))
        {
            goingAroundSomething = true;
            obstacle = other.gameObject;
        }
    }


    void Update () {
        Debug.Log(canMove);

        Vector3 pPos = player.transform.position;
        Vector3 rPos = transform.position;
        float distance = Mathf.Sqrt(Mathf.Pow(pPos.x - rPos.x, 2) + Mathf.Pow(pPos.z - rPos.z, 2));

        if(distance >= 9)
        {
            scared = false;
            kittenAnim.SetBool("Scared", false);
        }
           

        if (scared)
        {
            Vector2 nest2d = new Vector2(nest.transform.position.x, nest.transform.position.z);
            Vector2 self2d = new Vector2(transform.position.x, transform.position.z);
            Vector2 player2d = new Vector2(player.transform.position.x, player.transform.position.z);
            Vector3 lookDir;

            if (!MathUtil.isLineIntersectsCircleOrCloserToNest(self2d, nest2d, player2d, avoidRadius) && !goingAroundSomething)
            {
                lookDir = nest.transform.position;
            }
            else if( !goingAroundSomething)
            {
                Vector2 tangent1, tangent2;
                MathUtil.getTangentsFromPoint(self2d, player2d, avoidRadius,out tangent1,out tangent2);
                Vector2 choosenTangent = MathUtil.pickCloserTangent(tangent1, tangent2, self2d,nest2d, nest2d,true);
                lookDir = new Vector3(choosenTangent.x, transform.position.y, choosenTangent.y);
            }
            else
            { 
                if (obstacle != null)
                    lookDir = goAroundDir(obstacle);
                else
                    lookDir = nest.transform.position;

                if (checkIfObstacleIsSurpassed(obstacle))
                {
                    goingAroundSomething = false;
                }
            }

            transform.LookAt(lookDir);

            if (distance < 3f)
            {
                speed = fastestRockSpeed;
                fleeing = true;
                kittenAnim.SetBool("Fleeing", true);
            }
            else if (distance < 3.5f && !fleeing)
                speed = fasterRockSpeed;
            else if (distance > 6f)
            {
                speed = rockSpeed;
                kittenAnim.SetBool("Fleeing", false);
            }

            if (canMove)
            {
                transform.position += transform.forward * speed * Time.deltaTime;
            }
        }
	}

    public Vector3 goAroundDir(GameObject obstacle)
    {
        Vector2 nest2d = new Vector2(nest.transform.position.x, nest.transform.position.z);
        Vector2 self2d = new Vector2(transform.position.x, transform.position.z);
        Vector2 obstacle2d = new Vector2(obstacle.transform.position.x, obstacle.transform.position.z);
        Vector2 player2d = new Vector2(player.transform.position.x, player.transform.position.z);
        float avoidRadius = obstacle.GetComponent<ObstacleInfo>().radius;

        Vector2 tangent1, tangent2;
        MathUtil.getTangentsFromPoint(self2d, obstacle2d, avoidRadius, out tangent1, out tangent2);
        Vector2 chosenTangent = MathUtil.pickCloserTangent(tangent1, tangent2, self2d, player2d, nest2d,false);

        // ha a ket erinto tul kozel van egymashoz oda vissza valtogat a cica ezt keruli ez el
        
        return  new Vector3(chosenTangent.x, transform.position.y, chosenTangent.y);
        
    }

    private bool checkIfObstacleIsSurpassed(GameObject obstacle)
    {
        float avoidRadius = obstacle.GetComponent<ObstacleInfo>().radius;
        return !MathUtil.isLineIntersectsCircleOrCloserToNest(new Vector2(transform.position.x, transform.position.z),
                                                new Vector2(nest.transform.position.x, nest.transform.position.z),
                                                new Vector2(obstacle.transform.position.x, obstacle.transform.position.z),
                                                avoidRadius);
    }

    private void FixedUpdate()
    {
        Vector3 higherRayCastOrigin = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        Vector3 lowerRayCastOrigin = transform.position;

        if (kittenAnim.GetBool("Jumping"))
        {
            kittenAnim.SetBool("Jumping", false);
        }

        RaycastHit hit;

        Physics.Raycast(lowerRayCastOrigin, transform.forward, out hit);
       
        if(hit.collider != null && hit.collider.tag == "Shroom")
        {
            if(Vector3.Distance(hit.point, transform.position) < 1.6f)
            {
                GetComponent<Rigidbody>().AddForce(transform.up* 15);
                kittenAnim.SetBool("Jumping", true);
            }
        }

        
    }
}

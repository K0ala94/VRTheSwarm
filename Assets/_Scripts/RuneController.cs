﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RuneController : MonoBehaviour {

    private bool drawing = false;
    public GameObject sparklPrefab;
    private GameObject sparkl;
    private GameObject player;
    public GameObject fireBallPrefab;
    private Collider[] checkPoints;
    private GameManager gameManager;
    private int faults;
    public int checkPointCount = 0;
    private string resultDialogeText;
    public bool dragging = false;
    

    public void pointerExit()
    {
        destroySparkle();
        if (drawing)
            faults++;
    }

    public void pointerRelease()
    {
        drawing = false;
        destroySparkle();
    }

    public void startDrawing(BaseEventData data)
    {
        drawing = true;
        PointerEventData pointerData = data as PointerEventData;
        Vector3 pointerPos = pointerData.pointerCurrentRaycast.worldPosition;

        if (sparkl == null)
        {
            sparkl = Instantiate(sparklPrefab, pointerPos, Quaternion.Euler(0, 0, 0));
        }
    }

    public void drawTheRune(BaseEventData data)
    {
        PointerEventData pointerData = data as PointerEventData;
        Vector3 pointerPos = pointerData.pointerCurrentRaycast.worldPosition;

        if (sparkl == null)
        {
            sparkl = Instantiate(sparklPrefab, pointerPos, Quaternion.Euler(0,0,0));
        }

        sparkl.transform.position = pointerPos;
    }

    private void destroySparkle()
    {
        if (sparkl != null)
            DestroyImmediate(sparkl);
    }

    private void resetRune()
    {
        faults = 0;
        checkPointCount = 0;
        foreach (var cp in checkPoints)
        {
            cp.enabled = true;
        }
        transform.Find("RuneEnd").GetComponent<Collider>().enabled = true;
    }

    public void evaluateRunePerformance()
    {
        if (faults > 2 || checkPointCount < checkPoints.Length)
        {
            onRuneFail();
        }
        else
        {
            onRuneSuccess();
        }
    }

    private void onRuneFail()
    {
        Debug.Log("Failed: " + faults + "--" + checkPointCount);
        GetComponent<ResultDialogeController>().displayRsult("Failed: You had " + faults + " faults" +
                    " and " + (checkPoints.Length - checkPointCount) + " missed checkpoints");
        Invoke("resetRune", 3f);
    }

    private void onRuneSuccess()
    {
        if (gameManager.LearnRunesDone)
        {
            GameObject controller = GameObject.Find("GvrControllerPointer");
            Vector3 pos = controller.transform.position + controller.transform.forward * 1;
            Instantiate(fireBallPrefab, pos, Quaternion.identity);
            Destroy(gameObject, 0.1f);
            gameManager.spawnRune();
        }
        else
        {
            Debug.Log("Success: " + faults + "--" + checkPointCount);
            GetComponent<ResultDialogeController>().displayRsult("Congratulations you learnt a new spell");
            gameManager.spawnRune();
            Destroy(gameObject, 3f);
        }
    }
    

    void Start () {
        player = GameObject.Find("Player");
        checkPoints = GetComponents<Collider>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
	
	
	void Update () {
        Vector3 startRot = transform.rotation.eulerAngles;
        transform.LookAt(player.transform);
        Vector3 rot = transform.rotation.eulerAngles;
        transform.rotation= Quaternion.Euler(new Vector3(startRot.x, rot.y, startRot.z));
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultDialogeController : MonoBehaviour {

    public GameObject resulDialogePrefab;


    public void displayRsult(string text)
    {
        GameObject resultDialoge = Instantiate(resulDialogePrefab, new Vector3(transform.position.x + 0.5f,0.67f,transform.position.z), transform.rotation);
        GameObject resultDialogeText = GameObject.Find("RuneResultText");
        resultDialogeText.GetComponent<Text>().text = text;
        Destroy(resultDialoge, 4f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogeController : MonoBehaviour {

    public GameObject player;
        

	void Start () {
        player = GameObject.Find("Player");
        transform.LookAt(player.transform);
        transform.Rotate(new Vector3(0, 180, 0));
    }

    public void redirect()
    {
        transform.LookAt(player.transform);
        transform.Rotate(new Vector3(0, 180, 0));
    }

    public void setDialogeText(string text)
    {
        Text dialogeText =GameObject.Find("DialogeText").GetComponent<Text>();
        dialogeText.text = text;
    }
}

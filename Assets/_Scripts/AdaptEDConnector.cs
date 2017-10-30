using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdaptEDConnector: MonoBehaviour {

    private static AndroidJavaClass jc;
    private static AndroidJavaObject currentActivity;

    public static int  Meditation { get; set; }
    public static int Attention { get; set; }

    public readonly static string OFFLINE = "OFFLINE";
    public readonly static string UNRESPONSIVE = "UNRESPONSIVE";
    public readonly static string BAD_SIGNAL = "BAD_SIGNAL";
    public readonly static string ONLINE = "ONLINE";

    private GameObject sensorInfo;

    private void Start()
    {
        sensorInfo = GameObject.Find("SensorInfo");
        sensorInfo.SetActive(false);
    }


    public static void sendRuneFaultEvent(int faultCount, string runeType)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            currentActivity = jc.GetStatic<AndroidJavaObject>("currentActivity");
        }

        if (currentActivity != null) {
            currentActivity.Call("exitedRuneWhileDrawing", new object[] { faultCount, runeType });
         }
    }

    public static void sendEndgameStatistics(string avgPerRunes, float avgFaults, float avgAttention,
                                      float avgAttentionAtFault, float avgMeditationAtFault)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            currentActivity = jc.GetStatic<AndroidJavaObject>("currentActivity");
        }

        if (currentActivity != null)
        {
            currentActivity.Call("sendEndGameStatistics", new object[] { avgPerRunes, avgFaults,avgAttention,avgAttentionAtFault, avgMeditationAtFault });
        }
    }

    public void recieveAttention(int attention)
    {
        AdaptEDConnector.Attention = attention;
    }

    public void recieveMeditation(int meditation)
    {
        AdaptEDConnector.Meditation = meditation;
    }

    public void recieveSensorStatusInfo(string status)
    {
       if(BAD_SIGNAL.Equals(status) || OFFLINE.Equals(status) || UNRESPONSIVE.Equals(status))
        {
            Time.timeScale = 0;
            
            sensorInfo.transform.position = GameObject.Find("RestartButtonLocation").transform.position;
            sensorInfo.SetActive(true);
        }
       else if (ONLINE.Equals(status))
        {
            sensorInfo.SetActive(false);
            Time.timeScale = 1.0f;
        }
    }






}

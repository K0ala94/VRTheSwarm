using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdaptEDConnector {

    private static AndroidJavaClass jc;
    private static AndroidJavaObject currentActivity;


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

	
}

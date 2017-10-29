using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatistics {

    public static int Water { get; set; }
    public static int Fire { get; set; }
    public static int Void { get; set; }
    public static int Nature { get; set; }

    public static List<RuneFault> faultData { get; set; }

    public static List<int> attentionData { get; set; }

    public static void resetStats()
    {
        Water = 0;
        Fire = 0;
        Void = 0;
        Nature = 0;
        faultData = new List<RuneFault>();
        attentionData = new List<int>();
    }

    public static void addFault(RuneFault fault)
    {
        faultData.Add(fault);
    }

    public static void registerRuneType(string type)
    {
        switch (type)
        {
            case "Water":
                Water++;
                break;
            case "Fire":
                Fire++;
                break;
            case "Void":
                Void++;
                break;
            case "Nature":
                Nature++;
                break;
        }
    }

    public static void createEndgameStatistics()
    {
        string avgFaultByRune;
        float avgFault;
        float avgAttentionAtFault = 0;
        float avgMeditationAtFault = 0;

        int runeCount = 0;
        int waterFault = 0;
        int fireFault = 0;
        int natureFault = 0;
        int voidFault = 0;

        foreach(RuneFault f in faultData)
        {
            switch (f.Type)
            {
                case "Water":
                    waterFault++;
                    break;
                case "Fire":
                    fireFault++;
                    break;
                case "Void":
                    voidFault++;
                    break;
                case "Nature":
                    natureFault++;
                    break;
            }

            avgAttentionAtFault += f.Attention;
            avgMeditationAtFault += f.Meditation;
        }

        runeCount = (Water + Fire + Nature + Void) == 0 ? 0 : (Water + Fire + Nature + Void);
        avgMeditationAtFault = faultData.Count == 0 ? 0 :  (avgMeditationAtFault / faultData.Count);
        avgAttentionAtFault = faultData.Count == 0 ? 0 : (avgAttentionAtFault /faultData.Count);
        avgFault = faultData.Count == 0 ? 0 : (faultData.Count / runeCount);
        avgFaultByRune = "Water: " +  (Water == 0 ? 0 : (waterFault/Water)) + 
                         "; Fire: " + (Fire == 0 ? 0 : (fireFault / Fire)) +
                         "; Void: " + (Void == 0 ? 0 : (voidFault / Void)) +
                         "; Nature: " + (Nature == 0 ? 0 : (natureFault / Nature));

        AdaptEDConnector.sendEndgameStatistics(avgFaultByRune, avgFault, 0, avgAttentionAtFault, avgMeditationAtFault);
    }



}

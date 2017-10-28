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

    public void createEndgameStatistics()
    {

    }



}

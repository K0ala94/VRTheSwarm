using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneFault {

    public string Type { get; set; }
    public int Meditation { get; set; }
    public int Attention { get; set; }

    public RuneFault(string type, int med, int att)
    {
        this.Type = type;
        this.Meditation = med;
        this.Attention = att;
    }
}

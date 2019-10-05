using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config
{
    public static IDictionary<string, string> tagList = new Dictionary<string, string>()
    {
        { "Monster", "Monster" },
        { "Ground", "Ground" },
        { "Spawn Point", "Spawn Point" },
        { "Terminate Point", "Terminate Point" },
    };
}

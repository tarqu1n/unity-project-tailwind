using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config
{
    public static IDictionary<string, string> tagList = new Dictionary<string, string>()
    {
        { "Building", "Building" },
        { "Monster", "Monster" },
        { "Ground", "Ground" },
        { "Track", "Track" },
        { "Spawn Point", "Spawn Point" },
        { "Terminate Point", "Terminate Point" },
    };

    public enum ResourceTypes
    {
        Wood = 0
    }

    public enum MouseButtonMap
    {
        Left = 0,
        Right = 1,
        Middle = 2
    }
}

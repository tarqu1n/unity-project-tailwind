using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Waypoint : MonoBehaviour
{
    public float weight;
    public Waypoint[] linked;

    public abstract void GetNext();
}


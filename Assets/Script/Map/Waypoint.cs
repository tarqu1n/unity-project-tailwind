using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Waypoint : MonoBehaviour
{
    public LinkedWaypoint[] linked;

    public abstract Waypoint GetNext();
}

[System.Serializable]
public class LinkedWaypoint
{
    public float weight;
    public Waypoint waypoint;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicWaypoint : Waypoint
{
    public override Waypoint GetNext()
    {
        return GetWeightedRandom();
    }

    private Waypoint GetWeightedRandom()
    {
        if (linked.Length == 0)
            return null;

        float rand = Random.Range(0f, 1f);

        float cumulative = 0f;
        foreach (LinkedWaypoint w in linked)
        {
            cumulative += w.weight;
            if (rand <= cumulative)
                return w.waypoint;
        }

        return linked[linked.Length - 1].waypoint;
    }
}

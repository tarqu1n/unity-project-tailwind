using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveBehaviour : Behaviour
{
    public override void HandleSetMoveTarget() { }
    public override void HandleSetSelected() { }

    public override void HandleMoveTargetInRange() { }

    public override void HandleRecieveDamage(float damage) { }

    public new void HandleDie() { }

    public override void HandleCollisionWithTerminatePoint(Collider collision) { }
}

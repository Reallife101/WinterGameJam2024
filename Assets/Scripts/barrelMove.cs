using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barrelMove : projectileMove
{
    protected override void Start()
    {
        deactivateParry();
        hasBeenParryed = false;
        travelDistance = Mathf.Infinity;
    }
}

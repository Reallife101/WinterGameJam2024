using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barrelMove : projectileMove
{
    private void Start()
    {
        deactivateParry();
        hasBeenParryed = false;
    }
}

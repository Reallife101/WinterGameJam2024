using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHealth : health
{

    public override void TakeDamage(int i)
    {
        currentHealth-=i;
        if (currentHealth == 0)
        {
            Destroy(gameObject);
        }
    }

}

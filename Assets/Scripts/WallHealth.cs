using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WallHealth : health
{

    public override void TakeDamage()
    {
        --currentHealth;
        if (currentHealth == 0)
        {
            Destroy(gameObject);
        }
    }

}

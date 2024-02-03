using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyHealth : health
{
    public override void TakeDamage()
    {
        Destroy(gameObject);
    }

}

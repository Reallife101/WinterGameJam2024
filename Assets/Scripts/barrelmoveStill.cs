using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barrelmoveStill : barrelMove
{
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasBeenParryed || collision.gameObject.tag == "hurt")
        {
            explode();
        }

    }

    protected override void Start()
    {
        deactivateParry();
        hasBeenParryed = false;
        travelDistance = Mathf.Infinity;
    }
}

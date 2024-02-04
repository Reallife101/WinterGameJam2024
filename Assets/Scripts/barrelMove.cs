using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barrelMove : projectileMove
{
    [SerializeField] float blastRadius;

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        explode();

    }

    protected void explode()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, blastRadius);

        foreach (Collider2D col in colliders)
        {
            health h = col.GetComponent<health>();
            if (h != null)
            {
                h.TakeDamage(3);
            }
        }

        FindObjectOfType<parryMode>().GetComponent<parryMode>().removeObject(this);
    }
}

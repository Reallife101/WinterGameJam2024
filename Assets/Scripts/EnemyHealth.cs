using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : health
{
    public override void TakeDamage()
    {
        Destroy(gameObject);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == hurtTag))
        {
            if (collision.gameObject.GetComponent<projectileMove>() != null)
            {
                if (collision.gameObject.GetComponent<projectileMove>().GetParryed())
                {
                    TakeDamage();
                    Destroy(collision.gameObject);
                }
            }
            else
            {
                TakeDamage();
                Destroy(collision.gameObject);
            }
        }
    }
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == hurtTag))
        {
            if (collision.gameObject.GetComponent<projectileMove>() != null)
            {
                if (collision.gameObject.GetComponent<projectileMove>().GetParryed())
                {
                    TakeDamage();
                    Destroy(collision.gameObject);
                }
            }
            else
            {
                TakeDamage();
                Destroy(collision.gameObject);
            }
        }
    }

}

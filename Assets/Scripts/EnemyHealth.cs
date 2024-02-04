using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : health
{
    [SerializeField] private GameObject bubbleShield;
    [SerializeField] private GameObject deathExplosion;

    public override void TakeDamage(int i = 1)
    {
        currentHealth-=i;
        if (currentHealth <= 0) {
            if (deathExplosion != null)
            {
                Instantiate(deathExplosion, transform.position, Quaternion.identity, transform.parent);

            }
            Destroy(gameObject);
        }
        else if (currentHealth >= 1)
        {
            if (bubbleShield != null)
            {
                bubbleShield.SetActive(false);
            }
        }
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

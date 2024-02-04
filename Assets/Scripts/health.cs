using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public abstract class health : MonoBehaviour
{
    [SerializeField] protected int maxHealth;
    protected int currentHealth;
    [SerializeField] protected string hurtTag = "hurt";

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public string getHurtTag()
    {
        return hurtTag;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == hurtTag))
        {
            TakeDamage();
            Destroy(collision.gameObject);
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == hurtTag) )
        {
            TakeDamage();
            Destroy(collision.gameObject);
        }
    }

    public abstract void TakeDamage(int i = 1);

    private void Heal()
    {
        currentHealth++;
    }

    public int getHealth()
    {
        return currentHealth;
    }

    public int getMaxHealth()
    {
        return maxHealth;
    }

}

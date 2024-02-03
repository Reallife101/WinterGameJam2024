using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == hurtTag))
        {
            TakeDamage();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == hurtTag) )
        {
            TakeDamage();
        }
    }

    public abstract void TakeDamage();

    private void Heal()
    {
        currentHealth++;
    }

    public int getHealth()
    {
        return currentHealth;
    }

}

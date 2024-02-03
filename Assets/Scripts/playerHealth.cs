using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class playerHealth : health
{
    public static event Action PlayerHitEvent;
    public static event Action PlayerHealEvent;
    public static event Action PlayerDeathEvent;
    bool invul = false;
    [SerializeField] float invulTime;
    //[SerializeField] float timeSlowTime = 0.75f;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == getHurtTag()) && !invul)
        {
            if (collision.gameObject.GetComponent<projectileMove>() != null)
            {
                if (!collision.gameObject.GetComponent<projectileMove>().GetParryed())
                {
                    TakeDamage();
                }
            }
            else
            {
                TakeDamage();
            }
        }
    }
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == getHurtTag()) && !invul)
        {
            if (collision.gameObject.GetComponent<projectileMove>() != null)
            {
                if (!collision.gameObject.GetComponent<projectileMove>().GetParryed())
                {
                    TakeDamage();
                }
            }
            else
            {
                TakeDamage();
            }
        }
    }

    public override void TakeDamage()
    {
        StartCoroutine(invincibilityCoroutine());
        PlayerHitEvent?.Invoke();
        currentHealth--;
        //FMODUnity.RuntimeManager.PlayOneShot(takeDamageSound);
        if (currentHealth <= 0)
        {
            //Instantiate(explosion, transform.position, Quaternion.identity);
            PlayerDeathEvent?.Invoke();
            Destroy(transform.parent.gameObject);
        }
    }

    private void Heal()
    {
        currentHealth++;
        PlayerHealEvent?.Invoke();
    }

    IEnumerator invincibilityCoroutine()
    {
        invul = true;
        yield return new WaitForSeconds(invulTime);
        invul = false;
    }
}

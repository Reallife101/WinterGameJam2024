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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == getHurtTag()) && !invul)
        {
            StartCoroutine(invincibilityCoroutine());
            TakeDamage();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == getHurtTag()) && !invul)
        {
            StartCoroutine(invincibilityCoroutine());
            TakeDamage();
        }
    }

    public override void TakeDamage()
    {
        PlayerHitEvent?.Invoke();
        currentHealth--;
        Debug.Log("ouch");
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

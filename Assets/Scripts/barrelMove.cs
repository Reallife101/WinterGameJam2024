using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barrelMove : projectileMove
{
    [SerializeField] float blastRadius;
    [SerializeField] GameObject explosion;
    [SerializeField] FMODUnity.EventReference explodeSFX2;



    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        FMODUnity.RuntimeManager.PlayOneShot(explodeSFX2);
        explode();

    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "borders")
        {
            FindObjectOfType<parryMode>().GetComponent<parryMode>().removeObject(this);
            Debug.Log("destroy 2");
            Destroy(gameObject);
        }
        if (hasBeenParryed && (collision.gameObject.tag == "BossShield" || collision.gameObject.tag == "hurt"))
        {
            FMODUnity.RuntimeManager.PlayOneShot(explodeSFX2);
            explode();
        }
        if (hasBeenParryed && collision.gameObject.tag == "Shield")
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Player")
        {
            //DoDamage
            FindObjectOfType<parryMode>().GetComponent<parryMode>().removeObject(this);
        }
    }



    protected void explode()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, blastRadius);

        Debug.Log(colliders.Length);

        foreach (Collider2D col in colliders)
        {
            health h = col.GetComponent<health>();
            if (h != null)
            {
                Debug.Log("Killing: "+col.gameObject.name);
                h.TakeDamage(3);
            }
        }

        Instantiate(explosion, transform.position, Quaternion.identity, transform.parent);
        FindObjectOfType<parryMode>().GetComponent<parryMode>().removeObject(this);
    }
}

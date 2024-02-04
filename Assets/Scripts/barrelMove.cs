using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barrelMove : projectileMove
{
    [SerializeField] float blastRadius;
    [SerializeField] GameObject explosion;



    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        explode();

    }



    protected void explode()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, blastRadius);

        Debug.Log(colliders.Length);

        foreach (Collider2D col in colliders)
        {
            Debug.Log(col.gameObject.name);
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

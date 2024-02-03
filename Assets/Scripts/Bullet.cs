using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected float speed;
    [SerializeField] protected float destroyAfterTime = 2.5f;
    private Vector2 velocity;
    // Start is called before the first frame update
    virtual protected void Start()
    {
        velocity = (transform.right).normalized * speed;
        rb.velocity = velocity;
        Destroy(gameObject, destroyAfterTime);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "borders")
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Player")
        {
            //DoDamage
            //Destroy(gameObject);
        }
    }
}

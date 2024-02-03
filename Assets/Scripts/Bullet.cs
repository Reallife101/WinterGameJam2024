using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected float speed;
    private Vector2 velocity;
    // Start is called before the first frame update
    virtual protected void Start()
    {
        velocity = (transform.right).normalized * speed;
        rb.velocity = velocity;
        Destroy(gameObject, 2.5f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileMove : MonoBehaviour
{
    public float speed;
    public bool destroyAfterTime;
    public float destroytime;

    private float timer;

    private void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + transform.up * speed * Time.deltaTime;
        timer = timer + Time.deltaTime;

        if (destroyAfterTime && timer > destroytime)
        {
            Destroy(gameObject);
        }
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
            Destroy(gameObject);
        }
    }

}

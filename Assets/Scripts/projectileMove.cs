using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileMove : MonoBehaviour
{
    public bool destroyAfterTime;
    public float destroytime;

    private float timer=0;

    private bool beingParryed;
    public bool hasBeenParryed;

    


    // Velocity stuff
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected float speed;

    private RaycastHit2D nextWallHit;
    protected float travelDistance;
    private float distanceCovered;
    [SerializeField] protected int maxBounces = 2;
    private int bounceCount;
    private Vector2 velocity;

    //Handle mouse variables
    [SerializeField] GameObject ui;

    protected virtual void Start()
    {
        bounceCount = 0;
        velocity = (transform.up).normalized * speed;
        rb.velocity = velocity;
        deactivateParry();
        hasBeenParryed = false;
        if (velocity.magnitude >= .01f)
            OnDirectionChange();
    }
    public bool GetParryed()
    {
        return hasBeenParryed;
    }

    public void ActivateParry()
    {
        if (!hasBeenParryed)
        {
            beingParryed = true;
            if (ui != null)
            {
                ui.SetActive(true);
            }
        }
        
    }
    public void deactivateParry()
    {
        beingParryed = false;
        if (ui != null)
        {
            ui.SetActive(false);
        }
    }

    // Update is called once per frame
    protected void Update()
    {
        //transform.position = transform.position + transform.up * speed * Time.deltaTime;
        timer = timer + Time.deltaTime;
        distanceCovered += velocity.magnitude * Time.deltaTime;
        if (distanceCovered >= travelDistance)
        {
            onWallHit();
        }

        if (destroyAfterTime && timer > destroytime)
        {
            FindObjectOfType<parryMode>().GetComponent<parryMode>().removeObject(this);
            Debug.Log("destroy 1");
            Destroy(gameObject);
        }

        //handly parry image
        if(beingParryed)
        {
            //-----reposition image--------

            // Get mouse Position
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 dir = mousePos - gameObject.transform.position;
            ui.transform.rotation = Quaternion.Euler(0, 0, (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg)-90);

            if (Input.GetButtonDown("Fire1"))
            {
                hasBeenParryed = true;
                transform.rotation = Quaternion.Euler(0, 0, (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) - 90);
                velocity = (transform.up).normalized * speed;
                rb.velocity = velocity;
                deactivateParry();
                OnDirectionChange();
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "borders")
        {
            FindObjectOfType<parryMode>().GetComponent<parryMode>().removeObject(this);
            Debug.Log("destroy 2");
            Destroy(gameObject);
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

    private void OnDirectionChange()
    {
        nextWallHit = Physics2D.Raycast(transform.position, transform.up, Mathf.Infinity, LayerMask.GetMask("Wall"));
        distanceCovered = 0f;
        if (velocity.magnitude >= 0.001f && nextWallHit)
        {
            travelDistance = nextWallHit.distance;
        }
        else
        {
            travelDistance = Mathf.Infinity;
        }
    }

    private void onWallHit()
    {
        if (!hasBeenParryed)
        {
            Debug.Log("destroy 3");
            Destroy(gameObject);
        }
        else
        {
            ++bounceCount;
            if (bounceCount > maxBounces)
            {
                Debug.Log("destroy 4");
                Destroy(gameObject);
            }
            distanceCovered = 0;
            Vector2 reflected = Vector2.Reflect(transform.up, nextWallHit.normal);
            Debug.Log(reflected);
            transform.rotation = Quaternion.Euler(0, 0, (Mathf.Atan2(reflected.y, reflected.x) * Mathf.Rad2Deg) - 90);
            velocity = reflected * velocity.magnitude;
            transform.position = new Vector2(transform.position.x, transform.position.y) + velocity * .1f;
            rb.velocity = velocity;
            OnDirectionChange();
        }
        
    }
}

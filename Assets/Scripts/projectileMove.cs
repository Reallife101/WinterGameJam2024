using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileMove : MonoBehaviour
{
    public bool destroyAfterTime;
    public float destroytime;

    private float timer;

    private bool beingParryed;
    public bool hasBeenParryed;


    // Velocity stuff
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected float speed;
    private Vector2 velocity;

    //Handle mouse variables
    [SerializeField] GameObject ui;

    private void Start()
    {
        timer = 0;
        //velocity = (transform.up).normalized * speed;
        //rb.velocity = velocity;
        deactivateParry();
        hasBeenParryed = false;
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
    void Update()
    {
        transform.position = transform.position + transform.up * speed * Time.deltaTime;
        timer = timer + Time.deltaTime;

        if (destroyAfterTime && timer > destroytime)
        {
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
                deactivateParry();
            }
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
            //Destroy(gameObject);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parryMode : MonoBehaviour
{
    public bool isParrying;

    [SerializeField] GameObject parryVisual;
    [SerializeField] float parryRadius;

    private List<projectileMove> parryObjects;

    // Start is called before the first frame update
    void Start()
    {
        parryVisual.SetActive(false);
        Time.timeScale = 1.0f;
        isParrying = false;
        parryObjects = new List<projectileMove>();
    }

    void parryOn()
    {
        parryVisual.SetActive(true);
        Time.timeScale = 0.1f;
        isParrying = true;
    }

    void parryOff()
    {
        parryVisual.SetActive(false);
        Time.timeScale = 1f;
        isParrying = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Handle Parry
        if (Input.GetKeyDown(KeyCode.Space))
        {
            parryOn();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            parryOff();
        }

        // get
        if (parryObjects.Count>0)
        {
            if (isParrying)
            {
                parryObjects[0].ActivateParry();
            }
            else
            {
                parryObjects[0].deactivateParry();
            }
            
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log(parryObjects.Count);

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        projectileMove pm = other.GetComponent<projectileMove>();
        if (pm !=null && !parryObjects.Contains(pm))
        {
            parryObjects.Add(pm);
            // Do something when an object enters the trigger
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        projectileMove pm = collision.GetComponent<projectileMove>();
        if (pm != null && parryObjects.Contains(pm) && pm.hasBeenParryed)
        {
            parryObjects.Remove(pm);
            // Remove things that have been parried
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        projectileMove pm = other.GetComponent<projectileMove>();
        if (pm != null && parryObjects.Contains(pm))
        {
            parryObjects.Remove(pm);
            // Do something when an object exits the trigger
        }
    }

}

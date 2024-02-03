using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parryMode : MonoBehaviour
{
    public bool isParrying;

    [SerializeField] GameObject parryVisual;

    [SerializeField] healthBar parryBar;
    [SerializeField] float maxTime;

    private float currentTime;

    private List<projectileMove> parryObjects;

    // Start is called before the first frame update
    void Start()
    {
        parryVisual.SetActive(false);
        Time.timeScale = 10.0f;
        isParrying = false;
        parryObjects = new List<projectileMove>();
        parryBar.sliderMax(maxTime);

        currentTime = maxTime;
    }

    void parryOn()
    {
        parryVisual.SetActive(true);
        Time.timeScale = 1f;
        isParrying = true;
    }

    void parryOff()
    {
        parryVisual.SetActive(false);
        Time.timeScale = 10f;
        isParrying = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Handle Parry
        if (Input.GetKeyDown(KeyCode.Space) && !parryBar.isRecovering)
        {
            parryOn();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            parryOff();
        }

        // Do Parry Mechanics
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

        // Drain healthBar
        if(isParrying)
        {
            currentTime -= Time.deltaTime;

            //if it hits bottom, go to recovery
            if (currentTime <=0)
            {
                parryBar.isRecovering = true;
                parryOff();
            }

            currentTime = Mathf.Max(0f, currentTime);

        }
        else
        {
            currentTime += Time.deltaTime/20;

            //Stop Recovering
            if (parryBar.isRecovering && currentTime >= maxTime/2)
            {
                parryBar.isRecovering = false;
            }
            currentTime = Mathf.Min(maxTime, currentTime);
            
        }
        parryBar.setSlider(currentTime);
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

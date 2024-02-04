using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    private bool lateStartExecuted = false;

    [SerializeField] GameObject marker;
    [SerializeField] GameObject three;
    [SerializeField] GameObject two;
    [SerializeField] GameObject one;


    void Update()
    {
        if (!lateStartExecuted)
        {
            LateStart();
            lateStartExecuted = true;
        }

    }

    void LateStart()
    {
        StartCoroutine(deleteAfterDelay());
    }

    public void setRegTime()
    {
        Time.timeScale = 10f;
        Destroy(gameObject);
    }

    private IEnumerator deleteAfterDelay()
    {
        three.SetActive(false);
        two.SetActive(false);
        one.SetActive(false);
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0.1f;
        marker.SetActive(true);

        three.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        three.SetActive(false);
        two.SetActive(true);

        yield return new WaitForSeconds(0.1f);
        two.SetActive(false);
        one.SetActive(true);

        yield return new WaitForSeconds(0.1f);
        one.SetActive(false);
        marker.SetActive(false);


        Time.timeScale = 10f;
        Destroy(gameObject);
    }

}


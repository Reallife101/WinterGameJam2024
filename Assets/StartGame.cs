using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    private bool lateStartExecuted = false;

    [SerializeField] GameObject kill;
    [SerializeField] GameObject them;
    [SerializeField] GameObject all;


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
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0.1f;
        kill.SetActive(true);
        them.SetActive(false);
        all.SetActive(false);

        yield return new WaitForSeconds(0.1f);
        kill.SetActive(false);
        them.SetActive(true);
        all.SetActive(false);

        yield return new WaitForSeconds(0.1f);
        kill.SetActive(false);
        them.SetActive(false);
        all.SetActive(true);

        yield return new WaitForSeconds(0.1f);
        all.SetActive(false);


        Time.timeScale = 10f;
        Destroy(gameObject);
    }

}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killThemAll : MonoBehaviour
{

    [SerializeField] GameObject win;
    [SerializeField] GameObject lose;

    private void Start()
    {
        win.SetActive(false);
        lose.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");

        if (enemies.Length<=0)
        {
            Time.timeScale = 0f;
            win.SetActive(true);
        } else if (GameObject.FindGameObjectWithTag("Player") == null)
        {
            Time.timeScale = 0f;
            lose.SetActive(true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killThemAll : MonoBehaviour
{

    [SerializeField] GameObject win;
    
    // Update is called once per frame
    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");

        if (enemies.Length<=0)
        {
            Time.timeScale = 0f;
            win.SetActive(true);
        }
    }
}

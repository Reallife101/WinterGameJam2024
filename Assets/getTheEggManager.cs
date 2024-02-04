using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getTheEggManager : MonoBehaviour
{
    [SerializeField] GameObject win;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Time.timeScale = 0f;
        win.SetActive(true);
    }
}

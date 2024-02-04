using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getTheEggManager : MonoBehaviour
{
    [SerializeField] GameObject win;
    [SerializeField] GameObject lose;

    private void Start()
    {
        win.SetActive(false);
        lose.SetActive(false);
    }

    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player") == null)
        {
            Time.timeScale = 0f;
            lose.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Time.timeScale = 0f;
            win.SetActive(true);
        }
        
    }
}

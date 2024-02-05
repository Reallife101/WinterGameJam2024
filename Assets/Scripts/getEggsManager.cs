using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getEggsManager : MonoBehaviour
{
    [SerializeField] GameObject win;
    [SerializeField] GameObject lose;

    private void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player") == null)
        {
            Time.timeScale = 0f;
            lose.SetActive(true);
        }

        if (GameObject.FindGameObjectWithTag("Egg") == null)
        {
            Time.timeScale = 0f;
            win.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}

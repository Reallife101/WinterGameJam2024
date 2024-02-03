using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parryMode : MonoBehaviour
{
    [SerializeField] GameObject parryVisual;
    [SerializeField] float parryRadius;

    private List<GameObject> parryObjects;

    // Start is called before the first frame update
    void Start()
    {
        parryVisual.SetActive(false);
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // Handle Parry Visual
        if (Input.GetKeyDown(KeyCode.Space))
        {
            parryVisual.SetActive(true);
            Time.timeScale = 0.1f;
            
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            parryVisual.SetActive(false);
            Time.timeScale = 1f;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log(parryObjects.Count);

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!parryObjects.Contains(other.gameObject))
        {
            parryObjects.Add(other.gameObject);
            // Do something when an object enters the trigger
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (parryObjects.Contains(other.gameObject))
        {
            parryObjects.Remove(other.gameObject);
            // Do something when an object exits the trigger
        }
    }

}

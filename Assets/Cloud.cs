using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    [SerializeField]
    private GameObject gO;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.S))
        {
            gO.transform.position = new Vector3(transform.position.x, transform.position.y, -3);
        }
        else
        {
            gO.transform.position = new Vector3(transform.position.x, transform.position.y, 3);
        }
    }
}

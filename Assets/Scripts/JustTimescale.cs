using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JustTimescale : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        Time.timeScale = 10f;
    }
}

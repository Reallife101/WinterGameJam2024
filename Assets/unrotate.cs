using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unrotate : MonoBehaviour
{
    private void Update()
    {
        if (transform.parent != null)
        {
            transform.localRotation = Quaternion.Inverse(transform.parent.localRotation);
        }
    }
}

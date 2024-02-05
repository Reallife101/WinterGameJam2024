using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IHATEFMOD : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("NOFMOD"))
        {
            //call here
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBarrel : MonoBehaviour
{
    [SerializeField] private Vector3 force;
    [SerializeField] private bool randomizeY = false;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
    }
}

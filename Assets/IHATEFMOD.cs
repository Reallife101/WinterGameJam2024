using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IHATEFMOD : MonoBehaviour
{
    [SerializeField] FMODUnity.EventReference pidgeonSFX;

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.tag.Equals("NOFMOD"))
        {
            FMODUnity.RuntimeManager.PlayOneShot(pidgeonSFX);
        }
    }
}

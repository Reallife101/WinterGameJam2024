using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uiSFX : MonoBehaviour
{
    [SerializeField] FMODUnity.EventReference Button;

    public void callUIAudio()
    {
        FMODUnity.RuntimeManager.PlayOneShot(Button);
    }
}

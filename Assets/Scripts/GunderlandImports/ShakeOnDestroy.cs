using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeOnDestroy : MonoBehaviour
{
    [SerializeField] private float shakePower;
    [SerializeField] private float shakeTime;

    private void OnDestroy()
    {
        CinemachineShake.Instance.ShakeCamera(shakePower, shakeTime);
    }
}

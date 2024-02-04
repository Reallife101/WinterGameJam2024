using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ForceField : MonoBehaviour
{
    [SerializeField]
    private VisualEffect forceField;
    private float seconds;
    private bool inactive;
    [SerializeField]
    private bool test;
    [SerializeField]
    private parryMode pMode;
void Start()
{
    forceField = GetComponent<VisualEffect>();
}
public void Toggle()
{
        forceField.SetBool("Inactive", true);
       StartCoroutine(turnOff());
}
public void ActiveField()
{forceField.SetBool("Inactive", false);
}
private IEnumerator turnOff()
{
    yield return new WaitForSecondsRealtime(0.25f);
    
    pMode.parryVisual.SetActive(false);
    
}

}

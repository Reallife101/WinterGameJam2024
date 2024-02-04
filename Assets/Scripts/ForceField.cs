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
void Start()
{
    inactive = forceField.GetBool("Inactive");
}
public void Toggle(bool toggle)
{
    
    if (!toggle)
    {
        inactive = true;
       // StartCoroutine(turnOff());

    }
    else {
        gameObject.SetActive(true);
    }
}
private IEnumerator turnOff()
{
    yield return new WaitForSeconds(1f);
    inactive = false;
    gameObject.SetActive(false);
    
}
void Update()
{
    if (test)
    {
        Toggle(false);
        
        Debug.Log("HI");
        test = false;
    }
}
}

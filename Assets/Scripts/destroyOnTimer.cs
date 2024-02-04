using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyOnTimer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float time;
    void Start()
    {
        StartCoroutine(deleteAfterDelay());
    }

    private IEnumerator deleteAfterDelay()
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);

    }
}

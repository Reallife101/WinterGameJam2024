using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class imageRotate : MonoBehaviour
{
    [SerializeField] private List<RectTransform> images;
    [SerializeField] private float rotateTime;
    [SerializeField] private float rotateSize;
    private float timePassed = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        float rotation = Mathf.Cos(timePassed / rotateTime) * rotateSize;
        foreach (RectTransform item in images)
        {
            item.rotation = Quaternion.Euler(item.rotation.x, item.rotation.y, rotation);
        }

    }
}

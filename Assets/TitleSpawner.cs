using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject toSpawn;
    [SerializeField] private float delay;
    private float timer;
    // Start is called before the first frame update
    void Awake()
    {
        spawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0)
        {
            spawn();
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    private void spawn()
    {
        timer = delay;
        GameObject g = Instantiate(toSpawn);
        Destroy(g, 10f);

    }
}

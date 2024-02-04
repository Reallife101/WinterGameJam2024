using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnClass
{
    public GameObject enemyPrefab;
    public int weight;
}

public class SurviveManager : MonoBehaviour
{
    [SerializeField] float timeToSurvive;
    float timeLeft;
    [SerializeField]
    private List<EnemySpawnClass> enemies;
    [SerializeField]
    private List<GameObject> spawnPoints;
    [SerializeField]
    private float spawnRate;



    // Start is called before the first frame update
    void Start()
    {
        timeLeft = timeToSurvive;
    }

    // Update is called once per frame
    void Update()
    {
        timeToSurvive -= Time.deltaTime;
    }
}

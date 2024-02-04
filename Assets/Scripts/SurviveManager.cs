using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Rendering.CameraUI;

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
    private List<Transform> waypoints;
    [SerializeField]
    private float spawnRate;
    private float spawnRateProgress;


    public GameObject chooseEnemy()
    {
        int totalWeight = 0;

        foreach(EnemySpawnClass enemy in enemies)
        {
            totalWeight += enemy.weight;
        }

        int rndWeightValue = Random.Range(1, 1 + totalWeight);

        //Checking where random weight value falls
        var processedWeight = 0;
        foreach (EnemySpawnClass enemy in enemies)
        {
            processedWeight += enemy.weight;
            if (rndWeightValue <= processedWeight)
            {
                return enemy.enemyPrefab;
            }
        }

        Debug.LogError("Error: Weight machine broke!");

        return null;
    }


    // Start is called before the first frame update
    void Start()
    {
        timeLeft = timeToSurvive;
        spawnRateProgress = spawnRate;
    }

    // Update is called once per frame
    void Update()
    {
        timeToSurvive -= Time.deltaTime;
        spawnRateProgress -= Time.deltaTime;

        if(spawnRateProgress < 0 )
        {
            spawnRateProgress = spawnRate;

            foreach(GameObject spawnpoint in spawnPoints)
            {
                BasicEnemy enemy = Instantiate(chooseEnemy(), spawnpoint.transform.position, Quaternion.identity).GetComponent<BasicEnemy>();
            }
        }

    }
}

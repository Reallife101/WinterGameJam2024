using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Rendering.CameraUI;

[System.Serializable]
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
    private List<GameObject> waypoints;

    [SerializeField] GameObject win;
    [SerializeField] GameObject lose;

    [SerializeField] TMP_Text timeLeftText;

    [SerializeField] bool genericSpawner = false;

    private bool gameDone = false;

    public List<GameObject> WAYPOINTS { get { return waypoints; } }

    [SerializeField]
    private float spawnRate;
    private float spawnRateProgress;

    public static SurviveManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


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
        spawnRateProgress = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameDone) { return; }

        if (GameObject.FindGameObjectWithTag("Player") == null)
        {
            Time.timeScale = 0f;
            lose.SetActive(true);
            gameDone = true;
        }

        timeLeft -= Time.deltaTime;
        spawnRateProgress -= Time.deltaTime;

        timeLeftText.text = ((int)(timeLeft / 10)).ToString();

        if(spawnRateProgress < 0 )
        {
            spawnRateProgress = spawnRate;

            foreach(GameObject spawnpoint in spawnPoints)
            {
                Instantiate(chooseEnemy(), spawnpoint.transform.position, Quaternion.identity);
            }
        }

        if(timeLeft < 0 && !genericSpawner)
        {
            Time.timeScale = 0f;
            win.SetActive(true);
            gameDone = true;
        }

    }
}

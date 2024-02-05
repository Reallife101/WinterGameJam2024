using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Boss : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private GameObject leftPatrolPoint;
    [SerializeField] private GameObject rightPatrolPoint;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float shieldSpinRate;
    private int direction;

    [Header("Spray Attack")]
    [SerializeField] private float coneSize;
    [SerializeField] private float numBullets;
    [SerializeField] private float sprayDuration;
    [SerializeField] private float sprayWaves;
    [SerializeField] private float sprayWaveDelay;

    [Header("Barrel Attack")]
    [SerializeField] private float barrelDelay;
    [SerializeField] private int numBarrels;

    [Header("Minion Attack")]
    [SerializeField] private float spawnDelay;

    [Header("Spawn Points")]
    [SerializeField] private List<GameObject> farMinionSpawnPoints;
    [SerializeField] private List<GameObject> closeMinionSpawnPoints;
    [SerializeField] private List<GameObject> barrelSpawns;

    [Header("Prefabs")]
    [SerializeField] private GameObject barrel;
    [SerializeField] private GameObject bullets;
    [SerializeField] private GameObject reg;
    [SerializeField] private GameObject riot;
    [SerializeField] private GameObject bubble;


    [Header("refs")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject shield;


    private List<Action> attacks = new List<Action>();
    private GameObject player;
    private int phase;

    private void Awake()
    {
        attacks.Add(sprayAttack);
        attacks.Add(spawnMinions);
        attacks.Add(barrelAttack);
        player = GameObject.FindWithTag("Player");
        direction = 1;
        phase = 0;
    }

    void Start()
    {
        barrelAttack();
    }

    public void SetPhase2()
    {
        return;
    }

    // Update is called once per frame
    void Update()
    {
        float closest = Mathf.Min((transform.position - leftPatrolPoint.transform.position).magnitude,
            (transform.position - rightPatrolPoint.transform.position).magnitude, 4);
        rb.velocity = moveSpeed * Mathf.Lerp(.1f, 1f, closest / 4f) * direction * Vector2.right * Time.deltaTime;
        if (transform.position.x > rightPatrolPoint.transform.position.x)
        {
            direction = -1;
        }
        else if (transform.position.x < leftPatrolPoint.transform.position.x)
        {
            direction = 1;
        }
        shield.transform.Rotate(0, 0, shieldSpinRate * Time.deltaTime);
    }


    private bool chance(float c)
    {
        return UnityEngine.Random.Range(0f, 1f) < c;
    }

    

    private void doRandom(int dontInclude=-1)
    {
        List<int> choices = new List<int> { 0, 1, 2 };
        choices.Remove(dontInclude);
        Debug.Log(choices.Count);
        attacks[choices[UnityEngine.Random.Range(0, choices.Count)]].Invoke();
    }

    private void barrelAttack()
    {
        StartCoroutine(barrelRoutine());
    }

    private void spawnMinions()
    {
        farMinionSpawnPoints = farMinionSpawnPoints.OrderBy(x => UnityEngine.Random.value).ToList();
        closeMinionSpawnPoints = closeMinionSpawnPoints.OrderBy(x => UnityEngine.Random.value).ToList();
        StartCoroutine(spawnMinionRoutine());
    }

    private void sprayAttack()
    {
        StartCoroutine(sprayRoutine());
    }

    private IEnumerator sprayRoutine()
    {
        int sign = 1;
        for (int j = 0; j < sprayWaves; j++)
        {
            if (j % 2 == 0)
                sign = 1;
            else
                sign = -1;
            for (float i = -coneSize / 2; i <= coneSize / 2 + .01f; i += coneSize / numBullets)
            {
                Instantiate(bullets, transform.position, Quaternion.Euler(0, 0, i * sign + 180f));
                yield return new WaitForSeconds(sprayDuration / numBullets);
            }
            yield return new WaitForSeconds(sprayWaveDelay);
        }
        yield return new WaitForSeconds(3 * Time.timeScale);
        doRandom(0);
    }

    private IEnumerator spawnMinionRoutine()
    {
        foreach (GameObject go in farMinionSpawnPoints)
        {
            if (chance(.4f))
            {
                float prob = UnityEngine.Random.Range(0f, 1f);
                if (prob > .5)
                {
                    Instantiate(riot, go.transform.position, go.transform.rotation);
                }
                else if (prob > .25)
                {
                    Instantiate(bubble, go.transform.position, go.transform.rotation);
                }
                else if (prob > .25)
                {
                    Instantiate(reg, go.transform.position, go.transform.rotation);
                }
                yield return new WaitForSeconds(spawnDelay);
            }
        }

        foreach (GameObject go in closeMinionSpawnPoints)
        {
            if (chance(.4f))
            {
                float prob = UnityEngine.Random.Range(0f, 1f);
                if (prob > .4)
                {
                    Instantiate(bubble, go.transform.position, go.transform.rotation);
                }
                else
                {
                    Instantiate(reg, go.transform.position, go.transform.rotation);
                }
                yield return new WaitForSeconds(spawnDelay);
            }
        }
        yield return new WaitForSeconds(5 * Time.timeScale);
        doRandom(1);
    }

    private IEnumerator barrelRoutine()
    {
        for (int i = 0; i < numBarrels; ++i)
        {
            yield return new WaitForSeconds(barrelDelay);
            Vector3 aim = player.transform.position - barrelSpawns[i % barrelSpawns.Count].transform.position;
            barrelSpawns[i % barrelSpawns.Count].transform.rotation = Quaternion.Euler(0, 0, (Mathf.Atan2(aim.y, aim.x) * Mathf.Rad2Deg));
            Instantiate(barrel, barrelSpawns[i % barrelSpawns.Count].transform.position, barrelSpawns[i % barrelSpawns.Count].transform.rotation * Quaternion.Euler(0f, 0f, -90f));
        }
        yield return new WaitForSeconds(1 * Time.timeScale);
        doRandom(2);
    }

}

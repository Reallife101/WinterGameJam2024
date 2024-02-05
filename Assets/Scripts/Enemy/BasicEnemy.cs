using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemy : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float bulletOffset;
    [SerializeField] private float shootDelay;
    [SerializeField] private float burstDelay;
    [SerializeField] private int burstSize;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] private int pointValue = 300;
    private float shootTimer;
    private float burstTimer;
    private int burstNumber;
    private GameObject player;
    private bool aggroActive = false;

    [Header("StateMachineStuff")]
    public float detectionRange = 100000;
    [SerializeField] List<GameObject> waypoints = new List<GameObject>();
    private EnemyState state;
    public GameObject PLAYER { get { return player; }}

    private void Start()
    {
        if(SurviveManager.instance != null)
        {
            waypoints = SurviveManager.instance.WAYPOINTS;
        }
        else if (FindObjectOfType<Waypoints>() != null)
        {
            waypoints = FindObjectOfType<Waypoints>().GetComponent<Waypoints>().waypoints;
        }

        state = new PatrolState(waypoints, this, Random.Range(0, waypoints.Count));
        shootTimer = shootDelay;
        player = GameObject.FindWithTag("Player");
    }

    public void toggleAggro(bool aggro)
    {
        aggroActive = aggro;
    }

    public void SetDestination(Vector3 destination)
    {
        agent.SetDestination(destination);
    }

    private void Update()
    {
        doRotate();
        state = state.OnUpdate();
        tryShoot();
    }

    private void tryShoot()
    {
        if(!aggroActive) { return; }

        if (burstNumber >= burstSize)
        {
            burstTimer = burstDelay;
            burstNumber = 0;
        }
        else if (burstTimer < 0)
        {
            if (shootTimer <= 0)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, Mathf.Infinity, ~LayerMask.GetMask("ignoreEnemyRaycast", "Hole"));

                if (hit && hit.collider.gameObject.tag.Equals("Player"))
                {
                    shoot();
                }
            }
            else
            {
                shootTimer -= Time.deltaTime;
            }

        }
        else
        {
            burstTimer -= Time.deltaTime;
        }
    }

    private void doRotate()
    {
        if (player != null)
        {
            Vector3 aim = player.transform.position - transform.position;
            transform.rotation = Quaternion.Euler(0, 0, (Mathf.Atan2(aim.y, aim.x) * Mathf.Rad2Deg));
        }
        
    }

    private void shoot()
    {
        Instantiate(bullet, transform.position + transform.right * bulletOffset, transform.rotation*Quaternion.Euler(0f, 0f,-90f));
        shootTimer = shootDelay;
        burstNumber++;
    }

    private void OnDestroy()
    {
        pointManager.PM_Instance.GainPoint(pointValue);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.PlayerSettings;

public class BasicEnemy : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float bulletOffset;
    [SerializeField] private float shootDelay;
    [SerializeField] private float burstDelay;
    [SerializeField] private int burstSize;
    [SerializeField] NavMeshAgent agent;
    private float shootTimer;
    private float burstTimer;
    private int burstNumber;
    private GameObject player;
    private bool aggroActive = false;

    [Header("StateMachineStuff")]
    public LayerMask playerLayer;
    public LayerMask wallLayer;
    public float detectionRange = 100000;
    public float moveSpeed;
    [SerializeField] List<GameObject> waypoints = new List<GameObject>();
    private EnemyState state;
    public GameObject PLAYER { get { return player; }}

    private void Start()
    {
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
        state.OnUpdate();
        doRotate();
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
                RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, Mathf.Infinity, ~LayerMask.GetMask("ignoreEnemyRaycast"));

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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float shootDelay;
    private float shootTimer;
    private GameObject player;

    private void Start()
    {
        shootTimer = shootDelay;
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        doRotate();
        tryShoot();
    }

    private void tryShoot()
    {
        if (shootTimer <= 0)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right);
            if (hit.collider.gameObject.tag.Equals("Player"))
            shoot();
        }
        else
        {
            shootTimer -= Time.deltaTime;
        }
    }

    private void doRotate()
    {
        Vector3 aim = player.transform.position - transform.position;
        transform.rotation = Quaternion.Euler(0, 0, (Mathf.Atan2(aim.y, aim.x) * Mathf.Rad2Deg));
    }

    private void shoot()
    {
        Instantiate(bullet, transform.position, transform.rotation);
        shootTimer = shootDelay;
    }
}

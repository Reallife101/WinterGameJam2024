using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : EnemyState
{
    public PatrolState(List<GameObject> patrolPoints, BasicEnemy enemy, int patrolIndex) : base(patrolPoints, enemy, patrolIndex)
    {
        enemy.SetDestination(patrolPoints[patrolIndex].transform.position);
    }

    public override EnemyState OnUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(enemy.transform.position, enemy.PLAYER.transform.position, enemy.detectionRange, (1 << enemy.playerLayer) | (1 << enemy.wallLayer));

        Vector2 v = enemy.gameObject.transform.position - patrolPoints[patrolIndex].transform.position;
        if (v.magnitude < .01f)
        {
            int index = Random.Range(0, patrolPoints.Count);
            nextState = new PatrolState(patrolPoints, enemy, index);
            return base.OnUpdate();
        }

        else if (hit.collider != null && hit.collider.gameObject.tag == "Player")
        {
            nextState = new AggroState(patrolPoints, enemy, patrolIndex);
            return base.OnUpdate();
        }

        
        return base.OnUpdate();
    }
}

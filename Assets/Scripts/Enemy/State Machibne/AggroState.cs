using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggroState : EnemyState
{
    public AggroState(List<GameObject> patrolPoints, BasicEnemy enemy, int patrolIndex) : base(patrolPoints, enemy, patrolIndex)
    {
        enemy.toggleAggro(true);
    }

    public override EnemyState OnUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(enemy.transform.position, enemy.PLAYER.transform.position, enemy.detectionRange, (1 << enemy.playerLayer) | (1 << enemy.wallLayer));

        if (hit.collider == null || hit.collider.gameObject.tag != "Player")
        {
            enemy.toggleAggro(false);
            nextState = new ChaseState(patrolPoints, enemy, patrolIndex, enemy.PLAYER.transform.position);
            return base.OnUpdate();
        }

        return base.OnUpdate();
    }
}

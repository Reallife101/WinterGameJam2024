using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class AggroState : EnemyState
{
    public AggroState(List<GameObject> patrolPoints, BasicEnemy enemy, int patrolIndex) : base(patrolPoints, enemy, patrolIndex)
    {
        enemy.toggleAggro(true);
        Debug.Log("Aggro Switch");
    }
    public override EnemyState OnUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(enemy.transform.position, enemy.transform.right, enemy.detectionRange, ~LayerMask.GetMask("ignoreEnemyRaycast"));

        if (!hit || hit.collider.gameObject.tag != "Player")
        {
            enemy.toggleAggro(false);
            nextState = new ChaseState(patrolPoints, enemy, patrolIndex, enemy.PLAYER.transform.position);
            return base.OnUpdate();
        }

        return base.OnUpdate();
    }
}

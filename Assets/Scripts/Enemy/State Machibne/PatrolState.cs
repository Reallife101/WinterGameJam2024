using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PatrolState : EnemyState
{
    public PatrolState(List<GameObject> patrolPoints, BasicEnemy enemy, int patrolIndex) : base(patrolPoints, enemy, patrolIndex)
    {
        enemy.SetDestination(patrolPoints[patrolIndex].transform.position);
        Debug.Log("Patrol Switch");
    }
    public override EnemyState OnUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(enemy.transform.position, enemy.transform.right, enemy.detectionRange, ~LayerMask.GetMask("ignoreEnemyRaycast"));

        Vector2 v = enemy.gameObject.transform.position - patrolPoints[patrolIndex].transform.position;
        if (v.magnitude < .01f)
        {
            int index = Random.Range(0, patrolPoints.Count);
            nextState = new PatrolState(patrolPoints, enemy, index);
            return base.OnUpdate();
        }

        else if (hit && hit.collider.gameObject.tag == "Player")
        {
            nextState = new AggroState(patrolPoints, enemy, patrolIndex);
            return base.OnUpdate();
        }

        
        return base.OnUpdate();
    }
}

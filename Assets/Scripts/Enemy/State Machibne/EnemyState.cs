using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class EnemyState
{

    protected List<GameObject> patrolPoints;
    protected BasicEnemy enemy;
    protected EnemyState nextState;
    protected int patrolIndex;

    protected EnemyState(List<GameObject> patrolPoints, BasicEnemy enemy, int patrolIndex)
    {
        this.patrolPoints = patrolPoints;
        this.enemy = enemy;
        this.nextState = this;
        this.patrolIndex = patrolIndex % patrolPoints.Count;
    }

    public virtual EnemyState OnUpdate()
    {
        return nextState;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IEnemyState
{
    private Enemy enemy;

    private float patrolTimer;
    private float patrolDuration;

    public void Enter(Enemy _enemy)
    {
        this.enemy = _enemy;
        patrolDuration = 10f;
    }

    public void Execute()
    {
        Debug.Log("Patroling");
        Patrol();
        enemy.Move();
    }

    public void Exit()
    {
        
    }

    public void OntriggerEnter(Collider2D collision)
    {
        
    }

    private void Patrol()
    {
        enemy.animationState = Enemy.AnimationState.RUN;
        patrolTimer += Time.deltaTime;

        if (patrolTimer >= patrolDuration)
        {
            enemy.ChangeState(new IdleState());
        }
    }
}

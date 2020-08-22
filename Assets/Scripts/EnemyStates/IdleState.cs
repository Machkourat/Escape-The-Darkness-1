using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IEnemyState
{
    private Enemy enemy;

    private float idleTimer;
    private float idleDuration;

    public void Enter(Enemy _enemy)
    {
        this.enemy = _enemy;
        idleDuration = 5f;
    }

    public void Execute()
    {
        Idle();
    }

    public void Exit()
    {
        
    }

    public void OntriggerEnter(Collider2D _collision)
    {
        
    }

    private void Idle()
    {
        enemy.animationState = Enemy.AnimationState.IDLE;
        idleTimer += Time.deltaTime;

        if (idleTimer >= idleDuration)
        {
            enemy.ChangeState(new PatrolState());
        }
    }
}

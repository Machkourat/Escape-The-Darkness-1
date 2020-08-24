using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedState : IEnemyState
{
    private Enemy enemy;

    public void Enter(Enemy _enemy)
    {
        this.enemy = _enemy;
    }

    public void Execute()
    {
        if (true)
        {
            if (enemy.Target != null)
            {
                enemy.Move();
            }
            else
            {
                enemy.ChangeState(new IdleState());
            }
        }
    }

    public void Exit()
    {
        
    }

    public void OntriggerEnter(Collider2D _collision)
    {
        
    }
}

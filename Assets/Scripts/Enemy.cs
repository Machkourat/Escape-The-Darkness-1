using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum AnimationState { IDLE, RUN, JUMP, FALL, HURT, CROUCH }

    public AnimationState animationState;

    private IEnemyState currentState;

    private bool facingRight;

    private int movementSpeed;

    public Animator Anim { get; private set; }

    private void Start()
    {
        ChangeState(new IdleState());       
        facingRight = true;
        movementSpeed = 5;
        Anim = GetComponent<Animator>();
    }

    private void Update()
    {
        currentState.Execute();
        SetAnimation();
    }

    public void SetAnimation()
    {
        Anim.SetInteger("state", (int)animationState);
    }

    public void ChangeState(IEnemyState _newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = _newState;
        currentState.Enter(this);
    }

    public void Move()
    {
        transform.Translate(GetDirection() * (movementSpeed * Time.deltaTime));
    }

    public Vector2 GetDirection()
    {
        return facingRight ? Vector2.right : Vector2.left;//Short hand version of code written below

        //if (facingRight == true)
        //{
        //    return Vector2(1, 0);
        //}
        //else if (facingRight == false)
        //{
        //    return Vector2(-1, 0);
        //}
        //else
        //{
        //    return null;
        //}
    }
}
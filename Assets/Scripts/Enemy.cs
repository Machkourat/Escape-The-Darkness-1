using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum AnimationState { IDLE, RUN, JUMP, FALL, HURT, CROUCH }

    [SerializeField] private FieldOfView fieldOfView;

    [SerializeField] private Transform wayPointLeft = default;
    [SerializeField] private Transform wayPointRight = default;

    public AnimationState animationState;

    private IEnemyState currentState;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private bool facingRight;
    private int movementSpeed;

    public GameObject Target { get; set; }

    public Animator Anim { get; private set; }

    public FieldOfView FieldOfView { get => fieldOfView; set => fieldOfView = value; }

    private void Start()
    {
        ChangeState(new IdleState());
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        facingRight = true;
        movementSpeed = 5;
        Anim = GetComponent<Animator>();
    }

    private void Update()
    {
        currentState.Execute();
        LookAtTarget();
        SetAnimation();

        FieldOfView.SetOrigin(transform.position);
        FieldOfView.SetAimDirection(GetDirection());
    }

    private void LookAtTarget()
    {
        if (Target != null)
        {
            float xDir = Target.transform.position.x - transform.position.x;

            if (xDir < 0 && facingRight || xDir > 0 && !facingRight)
            {
                

                ChangeDirection();
            }
        }
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

        //if (animationState == 0)
        //{
            
        //}
        //if (animationState == 1)
        //{

        //}
    }

    public void Move()
    {
        transform.Translate(GetDirection() * (movementSpeed * Time.deltaTime));
    }

    public Vector2 GetDirection()
    {
        return facingRight ? Vector2.right : Vector2.left;
    }

    public void ChangeDirection()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        currentState.OntriggerEnter(collision);
    }
}
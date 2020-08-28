using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum AnimationState { IDLE, RUN, JUMP, FALL, HURT, CROUCH }

    //[SerializeField] private FieldOfView fieldOfView;

    

    


    [SerializeField] private Transform wayPointLeft = default;
    [SerializeField] private Transform wayPointRight = default;

    public AnimationState animationState;

    private IEnemyState currentState;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private bool facingRight;
    private int movementSpeed;

    [SerializeField] private PlayerController player;
    [SerializeField] private Transform prefabFieldOFView;
    [SerializeField] private float fov = 40f;
    [SerializeField] private float viewDistance = 15f;

    //[SerilaizedField] private Vector3 aimDirection;
    private FieldOfView fieldOfView;



    public GameObject Target { get; set; }

    public Animator Anim { get; private set; }

    //public FieldOfView FieldOfView { get => fieldOfView; set => fieldOfView = value; }

    private void Start()
    {
        ChangeState(new IdleState());
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        facingRight = true;
        movementSpeed = 5;
        Anim = GetComponent<Animator>();

        fieldOfView = Instantiate(prefabFieldOFView, null).GetComponent<FieldOfView>();
        fieldOfView.SetFoV(fov);
        fieldOfView.SetViewDistance(viewDistance);

    }

    private void Update()
    {
        currentState.Execute();
        LookAtTarget();
        SetAnimation();

        //FieldOfView.SetOrigin(transform.position);
        //FieldOfView.SetAimDirection(GetDirection());
        if (fieldOfView != null)
        {
            fieldOfView.SetOrigin(transform.position);
            fieldOfView.SetAimDirection(GetDirection());
        }
        
        Debug.DrawLine(transform.position, transform.position + GetDirection() * 10f);

        FindTargetPlayer();
    }

    private void FindTargetPlayer()
    {
        if (Vector3.Distance(GetPosition(), player.GetPosition()) < viewDistance)
        {
            // Player inside ViewDistance
            Debug.Log("Attack1");

            Vector3 dirToPlayer = (player.GetPosition() - GetPosition()).normalized;

            if (Vector3.Angle(GetDirection(), dirToPlayer) < fov / 2f)
            {
                Debug.Log("Attack2");
            }
                // Use aimDirection for 360 degree. 
            //{
            //    Debug.Log("Attack2");

                //    //Player inside FieldofView
                //    RaycastHit2D raycastHit2D = Physics2D.Raycast(GetPosition(), dirToPlayer, viewDistance);

                //    if (raycastHit2D.collider != null)
                //    {
                //        Debug.Log("Attack1");

                //        //Player hit something
                //        if (raycastHit2D.collider.GetComponent<PlayerController>() != null)
                //        {
                //            Debug.Log("Hit");

                //            //Hit Player
                //            movementSpeed = movementSpeed * 2;
                //            ChangeState(new RangedState());

                //        }
                //        else
                //        {
                //            Debug.Log("NoAttack");

                //            //Hit Something Else
                //        }
                //    }
                //}            
        }
    }

    public Vector3 GetPosition()
    {
        return transform.position;
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

    public Vector3 GetDirection()
    {
        return facingRight ? Vector3.right : Vector3.left;
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
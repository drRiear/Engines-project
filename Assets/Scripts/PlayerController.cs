using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    #region Variables
    [SerializeField] private PlayerStats myStats;
    private Animator animator;
        //simple movement vars
    [SerializeField] private float jumpPower = 500;
    private Rigidbody2D rb;
        //isOnGround vars
    private CircleCollider2D circleCollider;
    [SerializeField] private LayerMask groundLayer;
    public float colliderMargin;
        
    private float direction;
        //Dash vars
    private enum DashState {ready, dashing, onCooldown};
    private Vector2 oldVelocity = Vector2.zero;
    private DashState dashState = 0;
    public float dashCooldownTimer = 0;
    public float dashTimer;
    
    #endregion

    #region Unity Events
    private void Start()
    { 
        rb = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();

        dashTimer = myStats.dashDuration;
    }
    private void FixedUpdate()
    {
        Movement();

        Dash();
    }
    private void Update()
    {
        Sprint();

        AnimationCtrl();

        Attack();

        if (myStats.healthPoints <= 0)
            Death();
    }

    
    #endregion

    #region Private Methods

    private void Movement()
    {
        direction = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(direction * myStats.runSpeed, rb.velocity.y);

        myStats.onGround = Physics2D.OverlapCircle(circleCollider.transform.position, circleCollider.radius + colliderMargin, groundLayer);

        if (Input.GetKeyDown(KeyCode.W) && myStats.onGround)
            rb.AddForce(Vector2.up * jumpPower * myStats.jumpHeight);
    }

    private void Sprint()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && myStats.staminaPoints > 0 && !myStats.inSprint)
        {
            myStats.runSpeed *= myStats.sprintMultiplier;
            myStats.inSprint = true;
            animator.speed *= myStats.sprintMultiplier;
        }
        if ((Input.GetKeyUp(KeyCode.LeftShift) && myStats.inSprint) || myStats.staminaPoints <= 0)
        {
            myStats.runSpeed = myStats.maxRunSpeed;
            myStats.inSprint = false;
            animator.speed /= myStats.sprintMultiplier;
        }
        //Stamina management    
        if (myStats.inSprint && myStats.staminaPoints > 0)
            myStats.staminaPoints -= myStats.staminaExpense * Time.deltaTime;
        if (!myStats.inSprint && myStats.staminaPoints < myStats.maxStaminaPoints)
            myStats.staminaPoints += myStats.staminaRegen * Time.deltaTime;
    }

    private void AnimationCtrl()
    {
        if (direction != 0)
        {
            Flip();
            animator.SetBool("Running", true);
        }
        else
            animator.SetBool("Running", false);

        animator.SetBool("Jumping", !myStats.onGround);
        animator.SetBool("Attacking", myStats.inAttack);
        animator.SetBool("Sprinting", myStats.inSprint);
        animator.SetBool("Dashing", myStats.inDash);


    }

    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Sign(direction);
        transform.localScale = scale;
    }

    private void Attack()
    {
        if (Input.GetMouseButtonDown(0))
            myStats.inAttack = true;
        if (Input.GetMouseButtonUp(0))
            myStats.inAttack = false;
    }
    
    private void Dash()
    {
        float dashForse = Mathf.Sign(direction) * myStats.maxRunSpeed * myStats.maxDashDistance;
        bool canIDash = myStats.staminaPoints > 20 && direction != 0 ? true : false;

        switch (dashState)
        {
            case DashState.ready :
                if (Input.GetKeyDown(KeyCode.Space) && dashCooldownTimer <= 0 && canIDash)
                {
                    oldVelocity = rb.velocity;
                    dashState = DashState.dashing;
                    myStats.staminaPoints -= 20.0f;
                }
                break;

            case DashState.dashing:
                dashTimer -= Time.deltaTime;
                dashCooldownTimer = myStats.dashCooldown;

                myStats.inDash = true;
                rb.velocity = new Vector2(dashForse, rb.velocity.y);
                if (dashTimer <= 0)
                {
                    rb.velocity = oldVelocity;
                    myStats.inDash = false;
                    dashState = DashState.onCooldown;
                }
                break;

            case DashState.onCooldown:
                dashCooldownTimer -= Time.deltaTime;
                dashTimer = myStats.dashDuration;
                if (dashCooldownTimer <= 0)
                    dashState = DashState.ready;
                break;
        }
        
    }

    private void Death()
    {
        Time.timeScale = 0;
        Destroy(gameObject);
    }

    #endregion
}

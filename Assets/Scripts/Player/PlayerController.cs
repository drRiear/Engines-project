using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    #region Variables
    private PlayerStats myStats;
    private Animator animator;
        //simple movement vars
    private float jumpPower = 500;
    private Rigidbody2D rb;
    private float direction;
        //Dash vars
    private Vector2 oldVelocity = Vector2.zero;
    [HideInInspector] public float dashCooldownTimer = 0;
    [HideInInspector] public float dashTimer;
        //Ulti vars
    private float ultiTimer = 0;
    #endregion

    #region Unity Events
    private void Start()
    { 
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        myStats = GetComponent<PlayerStats>();
        dashTimer = myStats.dashDuration;
    }
    private void FixedUpdate()
    {
        Movement();

        Dash();
    }
    private void Update()
    {
        StaminaManagement();

        Sprint();

        AnimationCtrl();

        Attack();

        UltiMechanic();

        if (!myStats.isAlive)
            Death();
        
    }
    #endregion

    #region Private Methods
    private void Movement()
    {
        direction = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(direction * myStats.runSpeed, rb.velocity.y);
        
        if (Input.GetKeyDown(KeyCode.W) && myStats.onGround)
            rb.AddForce(Vector2.up * jumpPower * myStats.jumpHeight);
    }

    private void Sprint()
    {
        if (direction == 0)
            return;
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
    }

    private void StaminaManagement()
    {
        if (myStats.inSprint && myStats.staminaPoints > 0)
            myStats.staminaPoints -= myStats.staminaExpense * Time.deltaTime;
        if (!myStats.inSprint && myStats.staminaPoints < myStats.maxStaminaPoints)
            myStats.staminaPoints += myStats.staminaRegen * Time.deltaTime;
    }
    
    private void AnimationCtrl()
    {
        if (direction != 0)
            Flip();

        animator.SetBool("Running", direction != 0);

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

        switch (myStats.dashState)
        {
            case PlayerStats.DashState.ready:
                if (Input.GetKeyDown(KeyCode.Space) && dashCooldownTimer <= 0 && canIDash)
                {
                    oldVelocity = rb.velocity;
                    myStats.dashState = PlayerStats.DashState.dashing;
                    myStats.staminaPoints -= 20.0f;
                }
                break;

            case PlayerStats.DashState.dashing:
                dashTimer -= Time.deltaTime;
                dashCooldownTimer = myStats.dashCooldown;

                myStats.inDash = true;
                rb.velocity = new Vector2(dashForse, rb.velocity.y);
                if (dashTimer <= 0)
                {
                    rb.velocity = oldVelocity;
                    myStats.inDash = false;
                    myStats.dashState = PlayerStats.DashState.onCooldown;
                }
                break;

            case PlayerStats.DashState.onCooldown:
                dashCooldownTimer -= Time.deltaTime;
                dashTimer = myStats.dashDuration;
                if (dashCooldownTimer < 0)
                {
                    myStats.dashState = PlayerStats.DashState.ready;
                    dashCooldownTimer = 0;
                }
                break;
        }
        
    }
    
    private void UltiMechanic()
    {

        switch (myStats.ultiState)
        {
            case PlayerStats.UltiState.charging:
                myStats.damage = myStats.baseDamage;
                ultiTimer = myStats.ultiDuration;
                if (myStats.CanIUlti)
                    myStats.ultiState = PlayerStats.UltiState.ready;
                break;
            case PlayerStats.UltiState.ready:
                if (Input.GetKey(KeyCode.Q))
                    myStats.ultiState = PlayerStats.UltiState.ulting;
                break;
            case PlayerStats.UltiState.ulting:
                ultiTimer -= Time.deltaTime;

                myStats.ultiPoints -= myStats.ultiCost * Time.deltaTime;
                if (myStats.ultiPoints <= 0.0f)
                    myStats.ultiPoints = 0.0f;

                Ulti();

                if (ultiTimer < 0)
                {
                    ultiTimer = 0;
                    myStats.ultiState = PlayerStats.UltiState.charging;
                }
                break;
        }
    }

    private void Ulti()
    {
        myStats.damage = myStats.ultiDamage;
    }

    private void Death()
    {
        Time.timeScale = 0;
        Destroy(gameObject);
    }

    #endregion

    
}

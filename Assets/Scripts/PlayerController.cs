using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    #region Variables
    [SerializeField] private PlayerStats playerStats;
    private Animator animator;

    [SerializeField] private float jumpPower = 500;
    private float speed;
    private bool inSprint = false;
    private Rigidbody2D rb;

    private CircleCollider2D circleCollider;
    private bool onGround = false;
    [SerializeField] private LayerMask groundLayer;

    private bool lookRight;
    private float direction;
    #endregion

    #region Unity Events
    private void Start()
    { 
        rb = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        Movement();
    }
    private void Update()
    {
        Sprint();

        AnimationCtrl();
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

        animator.SetBool("Jumping", !onGround);
    }
    #endregion

    #region Private Methods

    private void Movement()
    {
        direction = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(direction * playerStats.runSpeed, rb.velocity.y);

        onGround = Physics2D.OverlapCircle(circleCollider.transform.position, circleCollider.radius + 1.5f, groundLayer);

        if (Input.GetButtonDown("Jump") && onGround)
            rb.AddForce(Vector2.up * jumpPower * playerStats.jumpHeight);

        
    }

    private void Sprint()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && playerStats.staminaPoints > 0 && !inSprint)
        {
            playerStats.runSpeed *= playerStats.sprintMultiplier;
            inSprint = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) && inSprint)
        {
            playerStats.runSpeed = playerStats.maxRunSpeed;
            inSprint = false;
        }
        if (playerStats.staminaPoints <= 0)
        {
            playerStats.runSpeed = playerStats.maxRunSpeed;
            inSprint = false;
        }
            
        if (inSprint && playerStats.staminaPoints > 0)
            playerStats.staminaPoints -= playerStats.staminaExpense * Time.deltaTime;
        if (!inSprint && playerStats.staminaPoints < playerStats.maxStaminaPoints)
            playerStats.staminaPoints += playerStats.staminaRegen * Time.deltaTime;
    }

    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Sign(direction);
        transform.localScale = scale;
    }


    #endregion
}

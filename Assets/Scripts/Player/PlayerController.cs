﻿using System.Collections;
using UnityEngine;



public partial class PlayerController : MonoBehaviour {

    #region Variables
    private PlayerStats myStats;
        //simple movement vars
    private float jumpPower = 50;
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
        myStats = GetComponent<PlayerStats>();
        dashTimer = myStats.dashDuration;

        MessageDispatcher.AddListener(this);
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

        Attack();

        UltiMechanic();

        if (Input.GetKeyDown(KeyCode.Alpha1)) //rewrite 
            StartCoroutine(ZoomIn());

        if (Input.GetKeyDown(KeyCode.E))
            MessageDispatcher.Send(new Messages.Interaction());
    }
    
    #endregion

    private IEnumerator ZoomIn()
    {
        float startSize = Camera.main.orthographicSize;
        float t = 0.0f;
        while ( t < 1.0f)
        {
            Camera.main.orthographicSize = Mathf.Lerp(startSize, startSize / 2.0f, t);
            t += Time.deltaTime * 2.0f;
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        while (t > 0.0f)
        {
            Camera.main.orthographicSize = Mathf.Lerp(startSize, startSize / 2.0f, t);
            t -= Time.deltaTime * 2.0f;
            yield return null;
        }

    }

    #region Private Methods
    private void Movement()
    {
        direction = Input.GetAxis("Horizontal");
        myStats.onMove = direction != 0;
        rb.velocity = new Vector2(direction * myStats.runSpeed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.W) && myStats.onGround)
        {
            MessageDispatcher.Send(new Messages.PlayerJump());
            rb.AddForce(Vector2.up * jumpPower * myStats.jumpHeight);
        }
        Mathf.Clamp(rb.velocity.y, myStats.jumpHeight * -1, myStats.jumpHeight);

        if (direction != 0)
            Flip();
    }

    private void Sprint()
    {
        if (direction == 0)
            return;
        if (Input.GetKeyDown(KeyCode.LeftShift) && myStats.staminaPoints > 0 && !myStats.inSprint)
        {
            myStats.runSpeed *= myStats.sprintMultiplier;
            myStats.inSprint = true;
        }
        if ((Input.GetKeyUp(KeyCode.LeftShift) && myStats.inSprint) || myStats.staminaPoints <= 0)
        {
            myStats.runSpeed = myStats.maxRunSpeed;
            myStats.inSprint = false;
        }  
    }

    private void StaminaManagement()
    {
        if (myStats.inSprint && myStats.staminaPoints > 0)
            myStats.staminaPoints -= myStats.staminaExpense * Time.deltaTime;
        if (!myStats.inSprint && myStats.staminaPoints < myStats.maxStaminaPoints)
            myStats.staminaPoints += myStats.staminaRegen * Time.deltaTime;
    }
    

    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Sign(direction);
        transform.localScale = scale;
    }

    private void Attack()
    {
        myStats.inAttack = Input.GetMouseButton(0);
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

    #endregion
}

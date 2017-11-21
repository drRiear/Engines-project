using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    #region Private Variables
    private EnemyStats myStats;
    private Animator animator;
    private Rigidbody2D rb;
    
    private Vector2 startPosition;
    private State state = 0;
    
    //patrol
    private float distance;
    private bool patrolFlip = true;

    #endregion

    #region Public Variables
    public float patrolDistance;
    #endregion

    #region Enumerations
    public enum State { patrol}
    #endregion

    #region Unity Events
    private void Start()
    { 
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        myStats = GetComponent<EnemyStats>();

        startPosition = transform.position;
    }
    private void FixedUpdate()
    {
    }
    private void Update()
    {
        Movement();
        AnimationCtrl();
    }
    #endregion

    #region Private Methods
    private void Movement()
    {
        
        switch (state)
        {
            case State.patrol:
                Patrol();
                break;
        }
    }

    private void Patrol()
    {
        distance = Vector2.Distance(transform.position, startPosition);
        rb.velocity = Vector2.right * myStats.runSpeed;
        if (distance >= patrolDistance && patrolFlip)
        {
            myStats.runSpeed *= -1;
            patrolFlip = false;
        }
        else if (distance <= 1)
            patrolFlip = true;
    }
    private void AnimationCtrl()
    {
        if (rb.velocity.x != 0)
            Flip();

        animator.SetBool("Running", rb.velocity.x != 0);
    }

    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Sign(rb.velocity.x);
        transform.localScale = scale;
    }
/*
    private void Attack()
    {
        if (Input.GetMouseButtonDown(0))
            myStats.inAttack = true;
        if (Input.GetMouseButtonUp(0))
            myStats.inAttack = false;
    }
    private void Death()
    {
        Time.timeScale = 0;
        Destroy(gameObject);
    }
    */
    #endregion

}

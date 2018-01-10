using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    #region Private Variables
    private EnemyStats myStats;
    private Animator animator;
    private Rigidbody2D rb;

    private Vector2 startPosition;
    private Vector2 direction;
    private State state = State.patrol;

    //patrol
    private float distance;
    public bool patrolFlip = true;
    private float patrolDest;

    private Vector2 lastPlayerPosition;
    private Vector3 destination;
    #endregion

    #region Public Variables
    public float patrolDistance;
    public float warningRadius;
    public float agrRadius;
    #endregion

    #region Enumerations
    public enum State { patrol, warning, agressive }
    #endregion

    #region Unity Events
    private void Start()
    { 
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        myStats = GetComponent<EnemyStats>();
        StartCoroutine(MoveToDestination());

        startPosition = transform.position;
        destination.x = startPosition.x + patrolDistance;
        patrolDest = patrolDistance;
    }
    private void Update()
    {
        Movement();
        AnimationCtrl();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(startPosition,Vector2.down);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector2(warningRadius * 2.0f, 5.0f));
     
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector2(agrRadius * 2.0f, 5.0f));
    }
    #endregion
    
    #region Private Methods
    private void Movement()
    {
        switch (state)
        {
            case State.patrol:
                Patrol();
                LookForAPlayer(warningRadius);
                break;
            case State.warning:
                break;
            case State.agressive:
                animator.SetBool("Attacking", true);
                break;
        }
    }
    
    private void Patrol()
    {
        distance = Vector2.Distance(transform.position, startPosition);
        if (distance >= patrolDistance && patrolFlip)
        {
            patrolDest *= -1.0f;
            destination.x = startPosition.x + patrolDest;
            patrolFlip = false;
        }
        else if (distance <= 1)
            patrolFlip = true;
    }   

  /*  partol old
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
    */

    private void LookForAPlayer(float distance)
    {
        if (CharacterManager.Instance == null)
            return;

        var hits = Physics2D.RaycastAll(transform.position, rb.velocity, distance);
        foreach (var hit in hits)
            if (hit.transform.gameObject == CharacterManager.Instance.player)
            {
                lastPlayerPosition = hit.transform.position;
                destination = lastPlayerPosition;
                state = State.warning;
            }
    }
    
    private IEnumerator MoveToDestination()
    {
        distance = Vector2.Distance(transform.position, destination);
        while (distance != 0)
        {
            distance = Vector2.Distance(transform.position, destination);
            direction.x = Mathf.Sign(transform.position.x - destination.x) * -1;
            //rb.velocity = direction * myStats.runSpeed;
            
            if (distance <= 1)
                rb.velocity = Vector2.zero;
            yield return null;
        }
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
        scale.x = Mathf.Sign(direction.x);
        transform.localScale = scale;
    }
    #endregion

}

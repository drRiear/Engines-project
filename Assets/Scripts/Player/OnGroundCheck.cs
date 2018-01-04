using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGroundCheck : MonoBehaviour {

    #region Private Variables
    private PlayerStats myStats;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float radius;
    #endregion

    Rigidbody2D rb;

    #region Unity Events
    void Start ()
    {
        myStats = GetComponentInParent<PlayerStats>();
        rb = GetComponentInParent<Rigidbody2D>();
    }

	void FixedUpdate ()
    {
        myStats.onGround = Physics2D.OverlapCircle(transform.position, radius, groundLayer);

        if (myStats.onGround && rb.velocity.y < -1)
            MessageDispatcher.Send(new Messages.PlayerLanded());
        if (myStats.onGround && rb.velocity.y < myStats.jumpHeight * -2)
        {
            print(Mathf.Abs(rb.velocity.y));
            MessageDispatcher.Send(new Messages.PlayerHurted(Mathf.Abs(rb.velocity.y) / 100.0f));
        }
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    #endregion
}

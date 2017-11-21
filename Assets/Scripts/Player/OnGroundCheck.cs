using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGroundCheck : MonoBehaviour {

    #region Private Variables
    private PlayerStats myStats;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float radius;
    #endregion

    #region Unity Events
    void Start ()
    {
        myStats = GetComponentInParent<PlayerStats>();
    }
	void Update ()
    {
        myStats.onGround = Physics2D.OverlapCircle(transform.position, radius, groundLayer);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGroundCheck : MonoBehaviour {


    private PlayerStats myStats;
    private CircleCollider2D circleCollider;
    [SerializeField] private LayerMask groundLayer;

    void Start ()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        myStats = GetComponentInParent<PlayerStats>();
    }
	
	
	void Update ()
    {
        myStats.onGround = Physics2D.OverlapCircle(transform.position, circleCollider.radius, groundLayer);
    }
}

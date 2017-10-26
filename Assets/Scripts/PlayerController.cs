using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    #region Variables
    [SerializeField] private float maxSpeed = 3;
    [SerializeField] private float sprintMultiplier = 2;
    [SerializeField] private float jumpMultiplier = 2;
    private float jumpPower = 500;
    private float speed;
    private bool inSprint = false;
    private Rigidbody2D rb;

    private CircleCollider2D circleCollider;
    private bool onGround = false;
    [SerializeField] private LayerMask groundLayer;

    private bool lookRight = true;
    
    #endregion

    
    void Start ()
    { 
        rb = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        speed = maxSpeed;
    }
	
	void FixedUpdate ()
    {
        Movement();
    }

    private void Movement()
    {
        float direction = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(direction * maxSpeed, rb.velocity.y);

        onGround = Physics2D.OverlapCircle(circleCollider.transform.position, circleCollider.radius + 1.5f, groundLayer);

        if (Input.GetButtonDown("Jump") && onGround)
            rb.AddForce(Vector2.up * jumpPower * jumpMultiplier);

        if ((direction < 0 && !lookRight) || (direction > 0 && lookRight))
            Flip(direction);

        Sprint();
    }

    private void Sprint()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !inSprint)
        {
            maxSpeed *= sprintMultiplier;
            inSprint = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) && inSprint)
        {
            maxSpeed = speed;
            inSprint = false;
        }
    }

    private void Flip(float direction)
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Sign(direction);
        transform.localScale = scale;
        lookRight = !lookRight;
    }
}

using UnityEngine;
using System.Collections;

public class PlayerDashController : MonoBehaviour
{
    #region Private Variables
    private Rigidbody2D rb;
    private PlayerStats myStats;
    private float directionOfMove;
    private float dashCooldownTimer;

    ///<summary>
    ///I dont fukin` know what is this var. Just let me die. 
    ///</summary>
    private float coefficient = 400.0f;
    #endregion

    #region Inspector Variables
    [SerializeField] private KeyCode dashKey;
    #endregion

    #region Unity Events
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myStats = GetComponent<PlayerStats>();
        dashCooldownTimer = myStats.dashCooldown;
    }
    
    void Update()
    {
        directionOfMove = Input.GetAxisRaw("Horizontal");

        bool canIDash = (myStats.staminaPoints > myStats.dashCost && directionOfMove != 0.0f);

        if (myStats.canIControll && canIDash)
            Dash();
    }
    #endregion

    #region Private Methods
    private void Dash()
    {
        switch (myStats.dashState)
        {
            case PlayerStats.DashState.Ready:
                Ready();
                break;
            case PlayerStats.DashState.Dashing:
                Dashing();
                break;
            case PlayerStats.DashState.Cooldown:
                Cooldown();
                break;
        }
    }

    private void Ready()
    {
        dashCooldownTimer = myStats.dashCooldown;

        if(Input.GetKeyDown(dashKey))
            myStats.dashState = PlayerStats.DashState.Dashing;
    }
    private void Dashing()
    {
        rb.AddForce(Vector2.right * directionOfMove * myStats.dashMaxDistance * coefficient, ForceMode2D.Force); 
        myStats.staminaPoints -= myStats.dashCost;
        myStats.dashState = PlayerStats.DashState.Cooldown;
    }
    private void Cooldown()
    {
        dashCooldownTimer -= Time.deltaTime;

        if (dashCooldownTimer <= 0.0f)
            myStats.dashState = PlayerStats.DashState.Ready;
    }
    #endregion

    #region Old Shirt
    //Old
    //private void Cooldown()
    //{
    //    dashCooldownTimer -= Time.deltaTime;
    //    dashTimer = myStats.dashDuration;
    //    if (dashCooldownTimer < 0)
    //    {
    //        myStats.dashState = PlayerStats.DashState.ready;
    //        dashCooldownTimer = 0;
    //    }
    //}

    //private void Dashing(float dashForse)
    //{
    //    dashTimer -= Time.deltaTime;
    //    dashCooldownTimer = myStats.dashCooldown;
    //    rb.velocity = new Vector2(dashForse, rb.velocity.y);
    //    if (dashTimer <= 0)
    //    {
    //        rb.velocity = oldVelocity;
    //        myStats.dashState = PlayerStats.DashState.cooldown;
    //    }
    //}

    //private void Ready(bool canIDash)
    //{
    //    if (Input.GetKeyDown(dashKey) && dashCooldownTimer <= 0 && canIDash)
    //    {
    //        oldVelocity = rb.velocity;
    //        myStats.dashState = PlayerStats.DashState.dashing;
    //        myStats.staminaPoints -= 20.0f;
    //    }
    //}
#endregion

}

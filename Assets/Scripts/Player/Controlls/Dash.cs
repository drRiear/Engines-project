using UnityEngine;

namespace Player.Controlls
{
    public class Dash : MonoBehaviour
    {
        #region Private Variables
        private Rigidbody2D rb;
        private Stats myStats;
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
            myStats = GetComponent<Stats>();
            dashCooldownTimer = myStats.dashCooldown;
        }
    
        void Update()
        {
            directionOfMove = Input.GetAxisRaw("Horizontal");

            bool canIDash = (myStats.staminaPoints > myStats.dashCost && directionOfMove != 0.0f);

            if (myStats.canIControll && canIDash)
                SetStat();
        }
        #endregion

        #region Private Methods
        private void SetStat()
        {
            switch (myStats.dashState)
            {
                case Stats.DashState.Ready:
                    Ready();
                    break;
                case Stats.DashState.Dashing:
                    Dashing();
                    break;
                case Stats.DashState.Cooldown:
                    Cooldown();
                    break;
            }
        }

        private void Ready()
        {
            dashCooldownTimer = myStats.dashCooldown;

            if(Input.GetKeyDown(dashKey))
                myStats.dashState = Stats.DashState.Dashing;
        }
        private void Dashing()
        {
            rb.AddForce(Vector2.right * directionOfMove * myStats.dashMaxDistance * coefficient, ForceMode2D.Force); 
            myStats.staminaPoints -= myStats.dashCost;
            myStats.dashState = Stats.DashState.Cooldown;
        }
        private void Cooldown()
        {
            dashCooldownTimer -= Time.deltaTime;

            if (dashCooldownTimer <= 0.0f)
                myStats.dashState = Stats.DashState.Ready;
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
        //        myStats.dashState = Stats.DashState.ready;
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
        //        myStats.dashState = Stats.DashState.cooldown;
        //    }
        //}

        //private void Ready(bool canIDash)
        //{
        //    if (Input.GetKeyDown(dashKey) && dashCooldownTimer <= 0 && canIDash)
        //    {
        //        oldVelocity = rb.velocity;
        //        myStats.dashState = Stats.DashState.dashing;
        //        myStats.staminaPoints -= 20.0f;
        //    }
        //}
        #endregion

    }
}

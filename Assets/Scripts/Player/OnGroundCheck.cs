using UnityEngine;

namespace Player
{
    public class OnGroundCheck : MonoBehaviour {

        #region Private Variables
        private Rigidbody2D rb;
        private Stats myStats;
        #endregion

        #region Inspector Variables
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private float radius;
        #endregion


        #region Unity Events
        void Start ()
        {
            myStats = GetComponentInParent<Stats>();
            rb = GetComponentInParent<Rigidbody2D>();
        }

        void FixedUpdate ()
        {
            myStats.onGround = Physics2D.OverlapCircle(transform.position, radius, groundLayer);

            PainfulLand();
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, radius);
        }

        #endregion

        #region Private Methods
        private void PainfulLand()
        {
            if (myStats.onGround && rb.velocity.y < -1.0f)
                MessageDispatcher.Send(new Messages.Player.Landed());
            if (myStats.onGround && Mathf.Abs(rb.velocity.y) > myStats.jumpHeight * 2.0f)
                MessageDispatcher.Send(new Messages.Player.Hurted(CalculateLandDamage()));
        }
        private int CalculateLandDamage()
        {
            // damage = x% of Y-velocity
            float damagePercent = 0.05f;

            return (int)(Mathf.Abs(rb.velocity.y) * damagePercent);
        }
        #endregion
    }
}

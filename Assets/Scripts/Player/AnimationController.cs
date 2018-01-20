using UnityEngine;

namespace Player
{
    public class AnimationController : MonoBehaviour
    {
        #region Private Variables
        private Rigidbody2D rb;
        private Animator animator;
        private Stats myStats;
    
        private int sprintParamHash;
        //private int attackParamHash;
        private int deadParamHash; 
        private int hitParamHash;
        private int velocityXParamHash;
        private int velocityYParamHash;
        #endregion
        #region Public Variables
        public AudioController audioController;
    
        [Header("Animator Parameters")]
        public string sprintParamName = "Sprinting";
        //public string attackParamName = "Attacking";
        public string deadParamName = "Dead";
        public string hitParamName = "Hited";
        public string velocityXParamName = "Horizontal velocity";
        public string velocityYParamName = "Vertical velocity";
        #endregion
        #region Unity Events
        private void Awake()
        {
            MessageDispatcher.AddListener(this);

            rb = GetComponentInParent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            myStats = GetComponentInParent<Stats>();
                
            GetParamsHash();
        }

        private void GetParamsHash()
        {
            sprintParamHash = Animator.StringToHash(sprintParamName);
            //attackParamHash = Animator.StringToHash(attackParamName);

            deadParamHash = Animator.StringToHash(deadParamName);
            hitParamHash = Animator.StringToHash(hitParamName);
            velocityXParamHash = Animator.StringToHash(velocityXParamName);
            velocityYParamHash = Animator.StringToHash(velocityYParamName);
        }

        private void Update()
        {
            SetAnimatorParams();
        }
        #endregion

        #region Private Methods 
        private void SetAnimatorParams()
        {
            animator.SetBool(deadParamHash, !myStats.isAlive);

            float normalizedVelocityY = rb.velocity.normalized.y;
            animator.SetFloat(velocityXParamHash, Input.GetAxis("Horizontal"));
            animator.SetFloat(velocityYParamHash, normalizedVelocityY);
        
            int intSprint = myStats.inSprint ? 1 : 0;
            animator.SetFloat(sprintParamHash, intSprint);
        }
        private void Hurted(Messages.PlayerHurted message)
        {
            animator.SetTrigger(hitParamHash);
        }
        private void PlayFootstepsSound()
        {
            audioController.Footsteps();
        }
        #endregion
    }
}


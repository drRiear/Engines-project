using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    #region Variables
    private Animator animator;
    private PlayerStats myStats;

    public PlayerAudioController audioController;
    #endregion

    #region Unity Events
    private void Start()
    {
        animator = GetComponent<Animator>();
        myStats = GetComponentInParent<PlayerStats>();

        MessageDispatcher.AddListener(this);
    }
    private void Update()
    {
        AnimationCtrl();
    }
    #endregion

    #region Private Methods 
    private void AnimationCtrl()
    {
        animator.SetBool("Running", myStats.onMove);
        animator.SetBool("Sprinting", myStats.inSprint);

        animator.SetBool("Attacking", myStats.inAttack);

        animator.SetBool("Dead", !myStats.isAlive);
        
    }
    private void Jump(Messages.PlayerJump message)
    {
        animator.SetTrigger("Jump");
    }
    private void Drop(Messages.PlayerDroped message)
    {
        animator.SetTrigger("Drop");
    }
    private void Hurted(Messages.PlayerHurted message)
    {
        animator.SetTrigger("Hited");
    }
    private void PlayFootstepsSound()
    {
        audioController.Footsteps();
    }
    #endregion
}


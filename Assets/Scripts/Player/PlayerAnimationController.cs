using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    #region Variables
    private Animator animator;
    private PlayerStats myStats;
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
    private void Hurted(Messages.PlayerHurted message)
    {
        animator.SetTrigger("Hited");
    }
    #endregion
}


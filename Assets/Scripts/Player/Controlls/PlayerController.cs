using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Private Variables
    private PlayerStats myStats;
    private Rigidbody2D rb;

    private float jumpPower = 50.0f;
    private float direction;
    //Attack vars
    private float attackDelay;
    #endregion

    #region Inspector Variables
    [SerializeField] private GameObject knifePrefab;
    [Header("Keys")]
    [SerializeField] private KeyCode interactionKey;
    [SerializeField] private KeyCode sprintKey;
    [SerializeField] private KeyCode statsWindowKey;
    #endregion

    #region Unity Events
    private void Start()
    { 
        rb = GetComponent<Rigidbody2D>();
        myStats = GetComponent<PlayerStats>();

        MessageDispatcher.AddListener(this);

        attackDelay = myStats.attackDelay;
    }
    private void FixedUpdate()
    {
        if (myStats.canIControll)
            Movement();
    }
    private void Update()
    {
        if (!myStats.canIControll) return;

        Sprint();

        StaminaManagement();

        Attack();

        if (Input.GetKeyDown(interactionKey))
            MessageDispatcher.Send(new Messages.Interaction());

        if (Input.GetKeyDown(statsWindowKey))
            MessageDispatcher.Send(new Messages.StatsWindowOpened());
    }
    #endregion
    
    #region Private Methods
    private void Movement()
    {
        direction = Input.GetAxis("Horizontal");

        myStats.onMove = (direction != 0.0f);
        if (direction != 0.0f)
            FlipScale();

        MoveHorizontal();
        JumpAndLand();
    }
    
    private void MoveHorizontal()
    {
        transform.Translate(new Vector3(direction, 0.0f) * myStats.currentRunSpeed * Time.deltaTime);
    }

    private void JumpAndLand()
    {
        if (Input.GetButtonDown("Jump") && myStats.onGround)
        {
            MessageDispatcher.Send(new Messages.PlayerJump());
            rb.AddForce(Vector2.up * jumpPower * myStats.jumpHeight);
        }
        //if (Input.GetButtonDown("Jump") && !myStats.onGround)
        //{
        //    MessageDispatcher.Send(new Messages.PlayerJump());
        //    rb.AddForce(Vector2.down * jumpPower * myStats.jumpHeight);
        //}
    }

    private void Sprint()
    {
        if (direction == 0.0f)
            return;
        if (Input.GetKeyDown(sprintKey) && myStats.staminaPoints > 0 && !myStats.inSprint)
        {
            myStats.currentRunSpeed *= myStats.sprintMultiplier;
            myStats.inSprint = true;
        }
        if ((Input.GetKeyUp(sprintKey) && myStats.inSprint) || myStats.staminaPoints <= 0)
        {
            myStats.currentRunSpeed = myStats.maxRunSpeed;
            myStats.inSprint = false;
        }  
    }
    private void StaminaManagement()
    {
        if (myStats.inSprint && myStats.staminaPoints > 0)
            myStats.staminaPoints -= myStats.staminaUsage * Time.deltaTime;
        if (!myStats.inSprint && myStats.staminaPoints < myStats.maxStaminaPoints)
            myStats.staminaPoints += myStats.staminaRegen * Time.deltaTime;
    }
    private void FlipScale()
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Sign(direction);
        transform.localScale = scale;
    }
    private void Attack()
    {
        attackDelay -= Time.deltaTime;

        if (Input.GetMouseButton(0) && attackDelay <= 0)
        {
            attackDelay = myStats.attackDelay;
            myStats.inAttack = true;
            GameObject knife = Instantiate(knifePrefab, transform.position, Quaternion.identity);
            knife.GetComponent<ThrowingKnifeBehaviour>().damage = myStats.damage;

            if (knife.GetComponent<PlayerStatsReference>() == null)
                knife.AddComponent<PlayerStatsReference>().stats = myStats;
            else
                knife.GetComponent<PlayerStatsReference>().stats = myStats;

        }
    }
    private void DisableControlls(Messages.PlayerDead message)
    {
        myStats.canIControll = false;
    }
    private void EnableControlls(Messages.PlayerRevived message)
    {
        myStats.canIControll = true;
    }

    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {


    #region Variables

    [Header("HP/Stamina")]
    [Tooltip("Maximum of player health")]
    public float maxHealthPoints;
    [HideInInspector] public float healthPoints;
    public float maxStaminaPoints;
    [HideInInspector] public float staminaPoints;
    [HideInInspector] public float staminaUsage;
    [HideInInspector] public float staminaRegen;

    [Header("Attack")]
    public float attackDelay;
    public float baseDamage;
    public float damage;

    [Header("Movement")]
    public float maxRunSpeed;
    public float currentRunSpeed;
    public float sprintMultiplier;
    public float jumpHeight;

    [Header("Dash")]
    public float dashMaxDistance;
    public float dashCooldown;
    public float dashCost;
    [HideInInspector] public DashState dashState;

    [Header("Ultimate")]
    public float ultiCost;
    [HideInInspector] public float ultiPoints;
    public float ultiDuration;
    public float ultiDamage;
    [HideInInspector] public bool CanIUlti { get { return ultiPoints == ultiCost; } }
    [HideInInspector] public UltiState ultiState;

    //Bools
    [HideInInspector] public bool inAttack;
    [HideInInspector] public bool inSprint;
    [HideInInspector] public bool onMove;
    [HideInInspector] public bool onGround;
    [HideInInspector] public bool canIControll = true;
    [HideInInspector] public bool isAlive { get { return healthPoints > 0; } }

    //Death
    [HideInInspector] public Vector3 lastCrossPosition = new Vector3();
    #endregion

    private void Awake()
    {
        maxHealthPoints = 5.0f;
        healthPoints = maxHealthPoints;

        maxStaminaPoints = 100.0f;
        staminaPoints = maxStaminaPoints;
        staminaUsage = 5.0f;
        staminaRegen = 10.0f;

        attackDelay = 0.5f;
        damage = 1.0f;
        baseDamage = damage;

        maxRunSpeed = 20.0f;
        currentRunSpeed = maxRunSpeed;
        sprintMultiplier = 2.0f;
        jumpHeight = 20.0f;             //+- = rb.velocity.y

        dashMaxDistance = 10.0f;
        dashCooldown = 1.0f;
        dashCost = 20.0f;
        dashState = DashState.ready;

        ultiCost = 25.0f;
        ultiPoints = 0.0f;
        ultiDuration = 3.0f;
        ultiDamage = 3.0f;
        ultiState = 0;

        lastCrossPosition = transform.position;

        CharacterManager.Instance.player = gameObject;
        MessageDispatcher.AddListener(this);
    }

    #region Private Methods
    private void IncreaseStamina(float newMaxStamina)
    {
        if (newMaxStamina == maxStaminaPoints)
            return;

        maxStaminaPoints = newMaxStamina;
    }
    private void CrossUsed(Messages.Cross message)
    {
        healthPoints = maxHealthPoints;
        lastCrossPosition = transform.position;
    }
    private void Hurted(Messages.PlayerHurted message)
    {
        healthPoints -= message.damage;

        if (!isAlive)
            MessageDispatcher.Send(new Messages.PlayerDead(transform.position));      //Insert player revive controller
    }
    #endregion

    #region Enumerations
    public enum State { idle, moving, sprinting, attacking, dashing, ulting}
    public enum DashState { ready, dashing, cooldown };
    public enum UltiState { charging, ready, ulting };
    #endregion
}

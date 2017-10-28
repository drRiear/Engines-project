using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {


    #region Variables
    public float maxHealthPoints;
    [HideInInspector] public float healthPoints;
    public float maxStaminaPoints;
    [HideInInspector] public float staminaPoints;
    [HideInInspector] public float staminaExpense;
    [HideInInspector] public float staminaRegen;
    public float maxRunSpeed;
     public float runSpeed;
    public float sprintMultiplier;
    public float jumpHeight;


    #endregion

    void Awake()
    {
        maxHealthPoints = 5;
        healthPoints = maxHealthPoints;

        maxStaminaPoints = 20;
        staminaPoints = maxStaminaPoints;
        staminaExpense = 5;
        staminaRegen = 10;

        maxRunSpeed = 10;
        runSpeed = maxRunSpeed;
        sprintMultiplier = 2;
        jumpHeight = 2;
    }
}

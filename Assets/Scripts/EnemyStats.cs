using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour {


    #region Variables
    public float maxHealthPoints;
     public float healthPoints;
    public float maxRunSpeed;
    public float runSpeed;
    public float attackPower;

    #endregion
    
    void Awake ()
    {
        healthPoints = maxHealthPoints;

	}
	
}

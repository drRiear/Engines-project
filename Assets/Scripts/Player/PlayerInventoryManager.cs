﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerInventoryManager : MonoBehaviour {

    [HideInInspector] public float dropedCoins = 0.0f;
    [HideInInspector] public Inventory inventory = new Inventory();

    //public string datafileName = "Inventory";
    //public Inventory inv = new Inventory();
    //private DataManager dataManager;


    #region Unity Events
    private void Awake ()
    {
        //dataManager = new DataManager(datafileName);
        //dataManager.CreateJSONFile();
        //inv = dataManager.LoadFromJSON<Inventory>();

        MessageDispatcher.AddListener(this);
    }
    #endregion

    #region Private Methods
    private void AddCoins(Messages.CoinPicketUp message)
    {
        inventory.coins += message.cost;
        
        //dataManager.SaveToJSON<Inventory>(inventory);
    }
    private void AddSouls(Messages.EnemyDead message)
    {
        inventory.souls += message.souls;
    }
    private void Death(Messages.PlayerDead message)
    {
        dropedCoins = inventory.coins;
        inventory.coins = 0.0f;
        inventory.coins = 0.0f;
    }
    #endregion

}

[System.Serializable]
public class Inventory
{
    public float coins;
    public float souls;
}

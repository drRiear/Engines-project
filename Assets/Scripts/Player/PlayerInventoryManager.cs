using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerInventoryManager : MonoBehaviour {

    public string datafileName = "Inventory";

    [HideInInspector]public Inventory inventory = new Inventory();
    public Inventory inv = new Inventory();

    private DataManager dataManager;

    #region Unity Events
    private void Awake ()
    {
        dataManager = new DataManager(datafileName);
        //dataManager.CreateJSONFile();

        inv = dataManager.LoadFromJSON<Inventory>();

        MessageDispatcher.AddListener(this);

        inventory.coins = 0.0f;

    }
    #endregion

    #region Private Methods
    private void AddCoin(Messages.CoinPicketUp msg)
    {
        inventory.coins += msg.cost;
        
        //dataManager.SaveToJSON<Inventory>(inventory);
    }
    #endregion

}

[System.Serializable]
public class Inventory
{
    public float coins;
}

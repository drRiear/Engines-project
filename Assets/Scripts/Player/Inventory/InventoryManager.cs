using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Player.Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        #region Hidden Variables
        [HideInInspector] public Inventory dropedInventory;
        [HideInInspector] public Inventory inventory;
        #endregion

        #region Public Variables
        public string datafileName = "Inventory";
        #endregion

        #region Public Variables
        private DataManager dataManager;
        #endregion


        #region Unity Events
        private void Awake ()
        {
            MessageDispatcher.AddListener(this);

            InitiateSave();

            inventory = new Inventory();
        }
        #endregion

        #region Private Methods
        private void InitiateSave()
        {
            dataManager = new DataManager(datafileName);
            dataManager.CreateJSONFile();
        }
        #endregion

        #region Message Based Methods
        private void AddCoins(Messages.CoinPicketUp message)
        {
            inventory.coins += message.coins;
        }
        private void AddSouls(Messages.SoulsPicketUp message)
        {
            inventory.souls += message.souls;
        }
        private void Death(Messages.Player.Dead message)
        {
            dropedInventory = inventory;

            inventory = new Inventory();
        }

        private void CrossUsed(Messages.Cross message)
        {
            dataManager.SaveToJSON(inventory);
        }
        #endregion

    }
}
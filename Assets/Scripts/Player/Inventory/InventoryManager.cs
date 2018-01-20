using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Player.Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        #region Hidden Variables
        [HideInInspector] public float dropedCoins = 0.0f;
        [HideInInspector] public float dropedSouls = 0.0f;
        #endregion

        #region Public Variables
        public Inventory inventory;
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
        private void Death(Messages.PlayerDead message)
        {
            dropedCoins = inventory.coins;
            dropedSouls = inventory.souls;

            inventory.coins = 0.0f;
            inventory.souls = 0.0f;
        }

        private void CrossUsed(Messages.Cross message)
        {
            dataManager.SaveToJSON(inventory);
        }
        #endregion

    }
}
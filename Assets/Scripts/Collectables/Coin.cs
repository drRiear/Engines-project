using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    public float coinCost;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == CharacterManager.Instance.player)
        {
            MessageDispatcher.Send(new Messages.CoinPicketUp(coinCost));
            Destroy(gameObject);
        }
    }
}

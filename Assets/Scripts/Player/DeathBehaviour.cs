using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject deathPlacePrefab;

    public List<Component> a = MessageDispatcher._Listeners;
    

	private void Awake ()
    {
        MessageDispatcher.AddListener(this);
	}

    private void SpawnDeathPlace(Messages.PlayerDead msg)
    {
        Instantiate(deathPlacePrefab, msg.position, Quaternion.identity);
    }
}

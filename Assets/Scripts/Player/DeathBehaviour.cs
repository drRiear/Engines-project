using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject deathPlacePrefab;

	private void Awake ()
    {
        MessageDispatcher.AddListener(this);
	}

    private void SpawnDeathPlace(Messages.PlayerDead message)
    {
        Instantiate(deathPlacePrefab, message.position, Quaternion.identity);
        Invoke("Revive", 1.0f);
    }
    private void Revive()
    {
        Transform currentTransform = GetComponent<Transform>();
        currentTransform.position = GetComponent<PlayerStats>().lastCrossPosition;
        MessageDispatcher.Send(new Messages.PlayerRevived());
    }
}

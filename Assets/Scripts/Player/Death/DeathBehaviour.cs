using UnityEngine;

namespace Player.Death
{
    public class DeathBehaviour : MonoBehaviour
    {
        [SerializeField] private GameObject deathPlacePrefab;
        [SerializeField] private float reviveDelay;

        private void Awake()
        {
            MessageDispatcher.AddListener(this);
        }

        private void SpawnDeathPlace(Messages.PlayerDead message)
        {
            Instantiate(deathPlacePrefab, message.position, Quaternion.identity);
            Invoke("Revive", reviveDelay);
        }

        private void Revive()
        {
            Transform currentTransform = GetComponent<Transform>();
            currentTransform.position = GetComponent<Stats>().lastCrossPosition;
            MessageDispatcher.Send(new Messages.PlayerRevived());
        }
    }
}

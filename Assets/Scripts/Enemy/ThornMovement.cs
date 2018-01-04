using System.Collections;
using UnityEngine;

public class ThornMovement : MonoBehaviour {

    #region Private Variables
    private GameObject player;
    private PlayerStats playerStats;
    private ThornStats myStats;
    
    private bool onMove = false;
    private float distance;
    private Vector3 startPosition;
    #endregion

    #region Public Variables
    public ThornType type;
    public float agrRadius;
    [Tooltip("Only for returnable Thorns")]
    public float visionRaduis;
    #endregion

    #region Unity Events
    private void Start()
    {
        myStats = GetComponent<ThornStats>();
        
        player = CharacterManager.Instance.player;

        startPosition = transform.position;
    }
    private void Update()
    {
        if (type == ThornType.nonReturnable)
            CheckDistance_NR();
        else if (type == ThornType.returnable)
            CheckDistance_R();
        else
            Debug.LogError("Thorn type not selected.");
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, agrRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRaduis);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
            PlayerCollision();
    }
    #endregion


    #region Private Methods

    #region NonReturnable
        private void CheckDistance_NR()
        {
            distance = Vector2.Distance(transform.position, player.transform.position);
            if (distance <= agrRadius && !onMove)
                StartCoroutine(MoveToPlayer_NR());
        }
        private IEnumerator MoveToPlayer_NR()
        {
            onMove = true;
            while (transform.position != player.transform.position)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, myStats.speed * Time.deltaTime);
                yield return null;
            }
        }
    #endregion

    #region Returnable
        //Thorn with return to self Pos when player out of visible range
        Vector3 dest;
        bool agred = false;
        private void CheckDistance_R()
        {
            distance = Vector2.Distance(transform.position, player.transform.position);

            if (!onMove)
                StartCoroutine(MoveToPlayer_R());

            if (distance <= agrRadius)
                agred = true;
            else if (distance >= visionRaduis)
                agred = false;

            if (agred)
                dest = player.transform.position;
            else
                dest = startPosition;
        }
        private IEnumerator MoveToPlayer_R()
        {
            onMove = true;
            while (onMove)
            {
                transform.position = Vector3.MoveTowards(transform.position, dest, myStats.speed * Time.deltaTime);
                yield return null;
            }
        }
    #endregion

    private void PlayerCollision()
    {
        MessageDispatcher.Send(new Messages.PlayerHurted(myStats.damage));
        CharacterManager.Instance.thornsList.Remove(gameObject);
        Destroy(gameObject);
    }
    #endregion

    public enum ThornType { nonReturnable, returnable };
}
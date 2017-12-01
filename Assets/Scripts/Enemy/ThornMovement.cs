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
    public float agrRadius;
    //public float visionRaduis;
    #endregion

    #region unity Events
    private void Start()
    {
        myStats = GetComponent<ThornStats>();

        player = CharacterManager.Instance.player;
        playerStats = player.GetComponent<PlayerStats>();

        startPosition = transform.position;
    }
    private void Update()
    {
        distance = Vector2.Distance(startPosition, player.transform.position);

        CheckDistance();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, agrRadius);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            playerStats.healthPoints -= myStats.damage;
            CharacterManager.Instance.thornsList.Remove(gameObject);
            Destroy(gameObject);
        }
    }
    #endregion
    
    #region Private Methods
    private void CheckDistance()
    {
        if (distance <= agrRadius && !onMove)
            StartCoroutine(MoveToPlayer());
    }
    private IEnumerator MoveToPlayer()
    {
        onMove = true;
        while (transform.position != player.transform.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, myStats.speed * Time.deltaTime);
            yield return null;
        }
    }
    #endregion


    /*  Thorn with return to self Pos when player out of visible range
    Vector3 dest;
    bool agred = false;
    #region Try
    private void CheckDistance()
    {
        if (!onMove)
            StartCoroutine(MoveToPlayer());

        if (distance <= agrRadius)
            agred = true;
        else if (distance >= visionRaduis)
            agred = false;

        if (agred)
            dest = player.transform.position;
        else
            dest = startPosition;
    }
    private IEnumerator MoveToPlayer()
    {
        onMove = true;
        while (onMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, dest, myStats.runSpeed * Time.deltaTime);
            yield return null;
        }
    }
    #endregion
    */
}

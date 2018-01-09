using System.Collections;
using UnityEngine;

public class ThornMovement : MonoBehaviour {

    #region Private Variables
    private GameObject player;
    private ThornStats myStats;

    private Vector3 startPosition;
    private bool agred = false;
    #endregion

    #region Public Variables
    public ThornType type = ThornType.nonReturnable;
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

        MessageDispatcher.AddListener(this);
    }
    private void Update()
    {
        if (type == ThornType.nonReturnable)
            CheckDistance_NR();
        else if (type == ThornType.returnable)
            CheckDistance_R();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, agrRadius);

        if (type == ThornType.returnable)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, visionRaduis);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
            PlayerCollision();
    }
    #endregion


    #region Private Methods
    
    private void CheckDistance_NR()
    {
        var distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance <= agrRadius)
            agred = true;
        Move();
    }
    private void CheckDistance_R()
    {
        var distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance <= agrRadius)
            agred = true;
        else if (distance >= visionRaduis)
            agred = false;
        Move();
    }

    private void Move()
    {
        if (agred)
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, myStats.speed * Time.deltaTime);
        else if (!agred && transform.position != startPosition)
            transform.position = Vector3.MoveTowards(transform.position, startPosition, myStats.speed * Time.deltaTime);
    }

    private void PlayerCollision()
    {
        MessageDispatcher.Send(new Messages.PlayerHurted(myStats.damage));
        gameObject.SetActive(false);
    }
    private void CrossUsed(Messages.Cross message)
    {
        gameObject.SetActive(true);
        transform.position = startPosition;
        agred = false;
    }
    #endregion

    public enum ThornType { nonReturnable, returnable };
}
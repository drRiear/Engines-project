using UnityEngine; 

public class ChargingEnemyBehaviour : MonoBehaviour
{
    #region Private Variables
    private Rigidbody2D rb;
    private EnemyStats myStats;
    private Vector3 directionOfView
    {
        get
        {
            if (transform.localScale.x > 0)
                return transform.right;
            else if (transform.localScale.x < 0)
                return transform.right * -1.0f;
            else
                return Vector3.zero;
        }
    }
    private State state;
    private float distanceOfRaycast;
    private Vector3 startPosition;
    #endregion

    #region Inspector Variables
    [Tooltip("Head transform for getting eyes position.")]
    [SerializeField] private Transform head;
    #endregion

    #region Public Variables
    [Header("Distances")]
    public float distanceOfView;
    public float distanceOfAggression;
    [Tooltip("When enemy can start explode")]
    public float distanceOfExplosionTrigger;
    [Header("")]
    public float chargeSpeed;
    #endregion

    #region Unity Events
    private void OnDrawGizmos()
    {
        //View
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(head.position, directionOfView * distanceOfView);
        //Aggression
        Gizmos.color = Color.red;
        Gizmos.DrawRay(head.position, directionOfView * distanceOfAggression);
        //Explosion
        Gizmos.color = Color.black;
        Gizmos.DrawRay(head.position, directionOfView * distanceOfExplosionTrigger);
    }
    private void Start()
    {
        MessageDispatcher.AddListener(this);

        if (distanceOfExplosionTrigger > distanceOfAggression || distanceOfAggression > distanceOfView)
            Debug.LogWarning("One of distance variable set wrong.\nGameObject name: " + gameObject.name);

        myStats = GetComponent<EnemyStats>();
        rb = GetComponent<Rigidbody2D>();

        startPosition = transform.position;
        distanceOfRaycast = distanceOfView + 3.0f;
    }
    private void Update()
    {
        switch(state)
        {
            case State.idle:
                Idle();
                break;
            case State.warning:
                Warning();
                break;
            case State.charge:
                Charge();
                break;
            case State.explosion:
                Explosion();
                break;
        }

    }
    float distanceToPlayer;
    private void FixedUpdate()
    {
        distanceToPlayer = Vector3.Distance(CharacterManager.Instance.player.transform.position, transform.position);
        if(distanceToPlayer <= distanceOfRaycast)
            LookForPlayer();
    }
    private void LookForPlayer()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(head.position, directionOfView, distanceOfView + 2.0f);

        foreach (var hit in hits)
            if (hit.collider.gameObject == CharacterManager.Instance.player)
            {
                SetState(hit);
                break;
            }
    }
    private void SetState(RaycastHit2D playerHit)
    {
        if (playerHit.distance <= distanceOfView && playerHit.distance >= distanceOfAggression && state == State.idle)
            state = State.warning;
        else if (playerHit.distance <= distanceOfAggression && playerHit.distance >= distanceOfExplosionTrigger && state == State.warning)
            state = State.charge;
        else if (playerHit.distance <= distanceOfExplosionTrigger && state == State.charge)
            state = State.explosion;
        else if (playerHit.distance > distanceOfView && state == State.warning)
            state = State.idle;
    }
    #endregion

    #region Private Methods
    private void Idle()
    {
        rb.bodyType = RigidbodyType2D.Static;
        //turn on Idle animation
    }
    private void Warning()
    {
        rb.bodyType = RigidbodyType2D.Static;
        //turn on Warning animation
    }
    private void Charge()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.velocity = new Vector2(directionOfView.x * chargeSpeed, rb.velocity.y);
    }
    private void Explosion()
    {

    }
    #endregion

    #region Message based Methods
    private void Hurted(Messages.EnemyHurted message)
    {
        //Damade resist while charging
        if (state == State.charge)
            message.damage *= 0.50f; 

        myStats.healthPoints -= message.damage;

        if (!myStats.isAlive)
            Death();

    }
    private void Death()
    {
        MessageDispatcher.Send(new Messages.EnemyDead(myStats.souls));

        if (!myStats.canRevive)
            Destroy(gameObject);
        else
            gameObject.SetActive(false);
    }
    private void CrossUsed(Messages.Cross message)
    {
        if (!myStats.isAlive)
        {
            myStats.healthPoints = myStats.maxHealthPoints;
            gameObject.SetActive(true);
        }

        if (state != State.idle)
        {
            transform.position = startPosition;
            state = State.idle;
        }
    }
    #endregion

    public enum State { idle, warning, charge, explosion }
}

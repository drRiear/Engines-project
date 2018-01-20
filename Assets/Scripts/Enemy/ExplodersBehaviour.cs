using UnityEngine;

namespace Enemy
{
    public class ExplodersBehaviour : MonoBehaviour
    {
        #region Private Variables
        private Stats myStats;
        private Renderer _renderer;
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
        private Vector3 distanceToPlayer;
        private State state;
        private Vector3 startPosition;
        private float explosionTimer;
        #endregion

        #region Inspector Variables
        [SerializeField] private bool drawGizmos;
        [Tooltip("Head transform for getting eyes position.")]
        [SerializeField] private Transform head;
        [Header("Offset for distance to player")]
        [Tooltip("View distance + this offset = trigger area for starting raycasting.")]
        [SerializeField] private Vector2 distanceOffset;
        #endregion

        #region Public Variables
        [Header("Distances")]
        public float distanceOfView;
        public float distanceOfAggression;
        public float distanceOfExplosionTrigger;
        [Header("Speed")]
        public float chargeSpeed;
        [Header("Explosion")]
        public float explosionRadius;
        public float explosionForce;
        public float explosionDelay;
        #endregion
        
        #region Unity Events
        private void OnDrawGizmos()
        {
            if (!drawGizmos) return;

            Vector3 start = transform.position;
            if (head != null) start = head.position;

            //Explosion
            Gizmos.color = Color.grey;
            Gizmos.DrawWireSphere(transform.position, explosionRadius);
            //View
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(start, directionOfView * distanceOfView);
            //Aggression
            Gizmos.color = Color.red;
            Gizmos.DrawRay(start, directionOfView * distanceOfAggression);
            //Explosion
            Gizmos.color = Color.grey;
            Gizmos.DrawRay(start, directionOfView * distanceOfExplosionTrigger);
        }
        private void Awake()
        {
            distanceOffset = new Vector2(5.0f, 5.0f);
            distanceOfView = 15.0f;
            distanceOfAggression = 10.0f;
            distanceOfExplosionTrigger = 5.0f;
            chargeSpeed = 5.0f;
            explosionRadius = 10.0f;
            explosionDelay = 1.0f;
            explosionTimer = explosionDelay;
        }
        private void Start()
        {
            MessageDispatcher.AddListener(this);

            SetExceptions();

            myStats = GetComponent<Stats>();
            _renderer = GetComponent<Renderer>();

            startPosition = transform.position;
        }

        private void Update()
        {
            distanceToPlayer = (CharacterManager.Instance.player.transform.position - transform.position) * directionOfView.x;

            switch (state)
            {
                case State.Idle:
                    Idle();
                    break;
                case State.Warning:
                    Warning();
                    break;
                case State.Wharge:
                    Charge();
                    break;
                case State.Explosion:
                    Explosion();
                    break;
            }
        }

        private void FixedUpdate()
        {
            if ((distanceToPlayer.x <= distanceOfView + distanceOffset.x && distanceToPlayer.x > 0.0f) 
                && Mathf.Abs(distanceToPlayer.y) <= distanceOffset.y)
                LookForPlayer();
        }
        #endregion

        #region Private Methods
        private void SetExceptions()
        {
            if (distanceOfExplosionTrigger > distanceOfAggression || distanceOfAggression > distanceOfView)
                Debug.LogWarning("One of distance variable set wrong.\nGameObject name: " + gameObject.name);

            if (head == null)
            {
                Debug.Log("Head transform reference not setted. Now it equals parent transform.\nGameObject name: " +
                          gameObject.name);
                head = transform;
            }
        }
        private void LookForPlayer()
        {
            var hits = Physics2D.RaycastAll(head.position, directionOfView, distanceOfView + 2.0f);

            foreach (var hit in hits)
                if (hit.collider.gameObject == CharacterManager.Instance.player)
                {
                    SetState(hit);
                    break;
                }
        }
        private void SetState(RaycastHit2D playerHit)
        {
            if (playerHit.distance <= distanceOfView && playerHit.distance >= distanceOfAggression && state == State.Idle)
                state = State.Warning;
            else if (playerHit.distance <= distanceOfAggression && playerHit.distance >= distanceOfExplosionTrigger && state == State.Warning)
                state = State.Wharge;
            else if (playerHit.distance <= distanceOfExplosionTrigger && state == State.Wharge)
                state = State.Explosion;
            else if (playerHit.distance > distanceOfView && state == State.Warning)
                state = State.Idle;
        }
        private void Idle()
        {
            _renderer.material.color = Color.white;
        }
        private void Warning()
        {
            _renderer.material.color = Color.yellow;
        }
        private void Charge()
        {
            transform.Translate(directionOfView * chargeSpeed * Time.deltaTime);
            _renderer.material.color = Color.red;
        }
        private void Explosion()
        {
            _renderer.material.color = Color.gray;
            explosionTimer -= Time.deltaTime;
            if (explosionTimer <= 0.0f)
            {
                var colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

                if (colliders == null && colliders.Length == 0)
                {
                    Death();
                    return;
                }
                Explode(colliders);
            }
        }

        private void Explode(Collider2D[] colliders)
        {
            foreach (var _collider in colliders)
            {
                var rigidbody_component = _collider.GetComponent<Rigidbody2D>();
                if (rigidbody_component == null)
                    continue;

                var distance = _collider.transform.position - transform.position;
                var length = distance.magnitude;
                var direction = distance.normalized;
                var force = Mathf.Pow(explosionForce / length, 2);
                force = Mathf.Clamp(force, 0.0f, 75.0f);

                if (_collider.gameObject == CharacterManager.Instance.player)
                    MessageDispatcher.Send(new Messages.PlayerHurted(force / (explosionForce / 2)));

                rigidbody_component.AddForce(direction * force, ForceMode2D.Impulse);
            }

            myStats.healthPoints = 0.0f;
            Death();
        }

        private void Death()
        {
            MessageDispatcher.Send(new Messages.EnemyDead(gameObject));
            MessageDispatcher.Send(new Messages.SoulsPicketUp(myStats.souls));

            if (!myStats.canRevive)
                Destroy(gameObject);
            else
                gameObject.SetActive(false);
        }
        #endregion

        #region Message based Methods
        private void Hurted(Messages.EnemyHurted message)
        {
            //Damade resist while charging
            if (state == State.Wharge)
                message.damage *= 0.50f; 

            myStats.healthPoints -= message.damage;

            if (!myStats.isAlive)
                Death();

        }
        private void CrossUsed(Messages.Cross message)
        {
            if (!myStats.isAlive)
            {
                myStats.healthPoints = myStats.maxHealthPoints;
                gameObject.SetActive(true);
            }

            if (state != State.Idle)
            {
                explosionTimer = explosionDelay;
                transform.position = startPosition;
                state = State.Idle;
            }
        }
        #endregion

        public enum State { Idle, Warning, Wharge, Explosion }
    }
}

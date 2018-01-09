using UnityEngine;

public class ThrowingKnifeBehaviour : MonoBehaviour {
    
    #region Inspector Variables
    [Tooltip("Time in sec. after witch knife will be destroyed. Same as range.")]
    [SerializeField] private float lifeTime;
    [SerializeField] private float speed;
    #endregion
    

    #region Private variables
    private Vector3 difference;
    [HideInInspector] public float damage;
    #endregion

    private void Start()
    {
        difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference.Normalize();
        difference.z = 0.0f;
        
        float rot_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }

    private void Update ()
    {
        lifeTime -= Time.deltaTime;
        
        transform.position += difference * speed * Time.deltaTime;

        if (lifeTime <= 0)
            Destroy(gameObject);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CharacterManager.Instance.thornsList.Contains(collision.gameObject) || CharacterManager.Instance.enemiesList.Contains(collision.gameObject))
        {
            MessageDispatcher.Send(new Messages.EnemyHurted(collision.gameObject, damage));
            Destroy(gameObject);
        }
        //rewrite
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            Destroy(gameObject);
    }
}

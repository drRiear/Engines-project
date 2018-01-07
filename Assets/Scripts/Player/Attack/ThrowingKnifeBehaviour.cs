using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingKnifeBehaviour : MonoBehaviour {

    [Tooltip("Time in sec. after witch knife will be destroyed. Same as range.")]
    [SerializeField]private float lifeTime;
    [SerializeField]private float speed;
    
    private Vector3 mousePosition;

    Vector3 difference;

    private void Start()
    {
        mousePosition = Camera.main.ScreenPointToRay(Input.mousePosition).origin;

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
        if (collision.gameObject == CharacterManager.Instance.player)
            return;

        print("col");

        //MessageDispatcher.Send(Messages.EnemyHurted);
        Destroy(gameObject);
    }
}

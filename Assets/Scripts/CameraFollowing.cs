using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowing : MonoBehaviour {

    #region Variables
    [SerializeField] private GameObject player;
    #endregion

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Follow();
    }

    private void Follow()
    {
        Vector3 followPosition = player.transform.position;
        followPosition.z = transform.position.z;
        transform.position = followPosition;
    }
}

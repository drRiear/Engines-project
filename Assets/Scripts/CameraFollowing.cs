using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowing : MonoBehaviour {

    #region Variables
    [SerializeField] float verticalOffset;

    private Transform playerTransforn;
    #endregion

    void Start ()
    {
        playerTransforn = CharacterManager.Instance.player.transform;
	}
	void Update ()
    {
        Follow();
    }

    private void Follow()
    {
        Vector3 followPosition = playerTransforn.transform.position;
        followPosition.z = transform.position.z;
        followPosition.y += verticalOffset;
        transform.position = followPosition;
    }
}

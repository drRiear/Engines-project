using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triger : MonoBehaviour {


    public GameObject iteractionText;
    [SerializeField]private GameObject player;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hero")
        {
            iteractionText.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hero")
        {
            iteractionText.SetActive(true);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        //if (collision.gameObject.tag == "Cross" && Input.GetKeyDown(KeyCode.E))
            //player.Health = player.maxHealth;
    }
}

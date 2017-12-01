using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour {

    public static CharacterManager Instance { get; private set; }

    public GameObject player;
    public List<GameObject> npcList;        
    public List<GameObject> enemiesList;
    public List<GameObject> thornsList;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Debug.LogError("CharacterManager is already exist. Another object of this type was destroyed.");
            Destroy(gameObject);
        }
    }

}

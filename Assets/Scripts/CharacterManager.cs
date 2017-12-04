using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{


    #region
    public GameObject player;
    public List<GameObject> npcList;
    public List<GameObject> enemiesList;
    public List<GameObject> thornsList;
    #endregion


    private static CharacterManager characterManager;

    public static CharacterManager Instance
    {
        get
        {
            if (!characterManager)
            {
                characterManager = FindObjectOfType(typeof(CharacterManager)) as CharacterManager;

                if (!characterManager)
                    Debug.LogError("There needs to be one active CharacterManager script on a GameObject in your scene.");
            }
            return characterManager;
        }
    }
}

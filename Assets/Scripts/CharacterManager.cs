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

    #region Singleton Implementation
    private static CharacterManager _Instance;

    public static CharacterManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = (CharacterManager)FindObjectOfType(typeof(CharacterManager));

                if (FindObjectsOfType(typeof(CharacterManager)).Length > 1)
                    Debug.LogError("There needs to have only one active CharacterManager script on a GameObject in your scene.");


                if (_Instance == null)
                {
                    //Generic Implementation
                    //GameObject singleton = new GameObject();
                    //singleton.name = "Singleton<" + typeof(T).ToString() + ">";
                    //singleton.AddComponent<T>();
                    Debug.LogError("There needs to have active CharacterManager script on a GameObject in your scene.");
                }

            }
            return _Instance;
        }
    }
    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{


    public Button playButton;
    public Button exitButton;

    private int currentSceneIndex;

	private void Start ()
	{
	    currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

	    playButton.onClick.AddListener(delegate { Play();} );
	    exitButton.onClick.AddListener(delegate { Exit();} );
	}

    private void Play()
    {
        SceneManager.LoadScene(currentSceneIndex + 1)
    }

    private void Exit()
    {
        Application.Quit();
    }
}

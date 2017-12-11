using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerAudioController : MonoBehaviour {

    #region Private Vars

    [SerializeField] private AudioSource hurtSource;
    [SerializeField] private AudioSource jumpSource;
    [SerializeField] private AudioSource dropSource;
    [SerializeField] private AudioSource footstepsSource;
    [SerializeField] private List<AudioClip> footstepClips;
    
    #endregion


    #region Unity Events 

    private void OnEnable()
    {
        MessageDispatcher.AddListener(this);
    }

    #endregion

    #region Private Methods

    private void Hurted(Messages.PlayerHurted message)
    {
        hurtSource.Play();
    }
    private void Jump(Messages.PlayerJump message)
    {
        jumpSource.Play();
    }
    
    private void Drop(Messages.PlayerDroped message)
    {
        dropSource.Play();
    }
    #endregion

    #region Public Methods
    public void Footsteps()
    {
        if (footstepClips.Count == 0)
            Debug.LogError("No footsteps audio clips.");

        int index = Random.Range(0, footstepClips.Count);
        footstepsSource.clip = footstepClips[index];
        footstepsSource.Play();
    }
    #endregion
}

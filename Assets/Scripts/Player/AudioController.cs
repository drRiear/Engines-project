using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class AudioController : MonoBehaviour {

        #region Private Vars

        [SerializeField] private AudioSource hurtSource;
        [SerializeField] private AudioSource jumpSource;
        [SerializeField] private AudioSource landSource;
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
    
        private void Land(Messages.PlayerLanded message)
        {
            landSource.Play();
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
}

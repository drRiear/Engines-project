using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioAreasChanger : MonoBehaviour {

    [SerializeField] AudioMixerSnapshot caveSnapshot;
    [SerializeField] AudioMixerSnapshot forestSnapshot;

    public void EnterForest()
    {
        forestSnapshot.TransitionTo(1.0f);
    }
    public void EnterCave()
    {
        caveSnapshot.TransitionTo(1.0f);
    }
}

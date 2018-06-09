using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMusic : MonoBehaviour {

    SoundManager soundManager;
    public string trackName;

    // Use this for initialization
    void Start()
    {
        soundManager = SoundManager.instance;
        if (soundManager == null)
        {
            Debug.Log("No Sound manager found");
        }
        SoundManager.instance.PlaySound(trackName);
    }

    void OnDestroy()
    {
        if (soundManager == null)
        {
            Debug.Log("No Sound manager found");
        }
        SoundManager.instance.StopAudio(trackName);
    }
}

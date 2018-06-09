using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class SoundEffect
{
    public string name;
    public AudioClip audioClip;
    private AudioSource audioSource;
    public bool loop;

    public void SetSource(AudioSource source)
    {
        audioSource = source;
        audioSource.clip = audioClip;
        audioSource.loop = loop;
    }

    public void PlayAudio()
    {
        if (audioSource != null)
        {
            audioSource.volume = 1.0f;
            audioSource.pitch = 1.0f;
            audioSource.Play();
        }
    }

    public void StopAudio()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }

}

public class SoundManager : MonoBehaviour {

    public static SoundManager instance;

    [SerializeField]
    SoundEffect[] SoundArray;

    void Awake()
    {
        if (instance != null)
        {
            if(instance != this)
            {
                Destroy(this.gameObject);
            }            
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    void OnEnable()
    {
        for (int i = 0; i < SoundArray.Length; i++)
        {
            GameObject go = new GameObject("sound_" + i + "_" + SoundArray[i].name);
            go.transform.SetParent(this.transform);
            SoundArray[i].SetSource(go.AddComponent<AudioSource>());
        }
    }

    public void PlaySound(string soundName)
    {
        for (int i = 0; i < SoundArray.Length; i++)
        {
            if(SoundArray[i].name == soundName)
            {
                SoundArray[i].PlayAudio();
                return;
            }
        }
    }

    public void StopAudio(string soundName)
    {
        for (int i = 0; i < SoundArray.Length; i++)
        {
            if (SoundArray[i].name == soundName)
            {
                SoundArray[i].StopAudio();
                return;
            }
        }
    }

}

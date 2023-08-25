using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private string[] mixerGroups;

    private void Start()
    {
        if(GameObject.FindGameObjectsWithTag("Music").Length > 1)
        {
            if(instance != this)
                Destroy(gameObject);
        }
        else
        {
            foreach(string mixerGroup in mixerGroups)
            {
                if(PlayerPrefs.HasKey(mixerGroup))
                {
                    mixer.SetFloat(mixerGroup, Mathf.Log10(PlayerPrefs.GetFloat(mixerGroup)) * 20);
                }
                else
                {
                    PlayerPrefs.SetFloat(mixerGroup, 1f);
                    PlayerPrefs.Save();
                }
            }
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void UpdateVolume(string mixerGroup, float volume)
    {
        mixer.SetFloat(mixerGroup, Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat(mixerGroup, volume);
        PlayerPrefs.Save();
    }

    public float GetVolume(string mixerGroup)
    {
        return PlayerPrefs.HasKey(mixerGroup) ? PlayerPrefs.GetFloat(mixerGroup) : 1f;
    }
}

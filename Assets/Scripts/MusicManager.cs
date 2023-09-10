using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    private void Start()
    {
        if(GameObject.FindGameObjectsWithTag("Music").Length > 1)
        {
            if (instance != this)
            {
                Destroy(gameObject);
                return;
            }
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

        isTransitioning = false;

        AudioSource[] audioSources = GetComponents<AudioSource>();

        foreach(AudioSource audioSource in audioSources)
        {
            if(audioSource.clip != null)
            {
                musicSource = audioSource;
            } else
            {
                secondarySource = audioSource;
            }
        }
    }

    private void Update()
    {
        if (isTransitioning)
        {
            transitionTimer += Time.deltaTime;

            musicSource.volume = Mathf.Max(1f - (transitionTimer / transitionDuration), 0f);
            secondarySource.volume = Mathf.Min(transitionTimer / transitionDuration, 1f);

            if(transitionTimer >= transitionDuration)
            {
                AudioSource temp = musicSource;
                musicSource = secondarySource;
                secondarySource = temp;

                secondarySource.Stop();

                isTransitioning = false;
            }
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

    public void transitionMusic(AudioClip newMusicClip, float _transitionDuration)
    {
        secondarySource.clip = newMusicClip;
        secondarySource.Play();

        transitionDuration = _transitionDuration;
        transitionTimer = 0f;

        isTransitioning = true;
    }

    public static MusicManager instance { get; private set; }

    [SerializeField] private AudioMixer mixer;
    [SerializeField] private string[] mixerGroups;

    private AudioSource musicSource;
    private AudioSource secondarySource;

    private bool isTransitioning;
    private float transitionDuration;
    private float transitionTimer;
}

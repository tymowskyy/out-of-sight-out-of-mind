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

            float newVolume = -1 * (1 / (1 + Mathf.Pow(0.1f, (transitionTimer - 0.5f) * 2f))) + 1;

            musicSource.volume = Mathf.Max(newVolume, 0f);
            secondarySource.volume = Mathf.Min(1f - newVolume, 1f);

            if(transitionTimer >= transitionDuration)
            {
                AudioSource temp = musicSource;
                musicSource = secondarySource;
                secondarySource = temp;

                musicSource.volume = 1f;
                secondarySource.volume = 0f;

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

    public string getCurrentAudioClipName()
    {
        return musicSource.clip.name;
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

    [SerializeField] float musicVolumeDifference;
    [SerializeField] float sfxVolumeDifference;

    private AudioSource musicSource;
    private AudioSource secondarySource;

    private bool isTransitioning;
    private float transitionDuration;
    private float transitionTimer;
}

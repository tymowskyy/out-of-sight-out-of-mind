using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance { get; private set; }
    public bool isPaused { get; private set; }
    [SerializeField] private GameObject pauseMenu;
    [Header("Audio")]
    [SerializeField] private AudioMixer mixer;
    const string MIXER_MUSIC = "MusicVolume";
    const string MIXER_SFX = "SFXVolume";
    const string MIXER_MASTER = "MasterVolume";

    private void Awake()
    {
        isPaused = false;
        instance = this;
    }

    public void OpenPauseMenu()
    {
        isPaused = true;
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }

    public void ClosePauseMenu()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }

    public void ExitToMainMenu()
    {
        Time.timeScale = 1f;
        LevelManager.instance.LoadMainMenu();
    }

    public void UpdateMasterVolume(float volume)
    {
        mixer.SetFloat(MIXER_MASTER, Mathf.Log10(volume) * 20);
    }
    public void UpdateMusicVolume(float volume)
    {
        mixer.SetFloat(MIXER_MUSIC, Mathf.Log10(volume) * 20);
    }

    public void UpdateSFXVolume(float volume)
    {
        mixer.SetFloat(MIXER_SFX, Mathf.Log10(volume) * 20);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class MainMenu : MonoBehaviour
{

    [Header("Menu")]
    [SerializeField] private GameObject startingScreen;
    [SerializeField] private GameObject settings;
    [SerializeField] private GameObject levelSelector;
    [SerializeField] private GameObject levelButtonPrefab;

    [Header("Levels")]
    [SerializeField] private int levelsPerRow;
    [SerializeField] private float levelButtonPadding;
    [SerializeField] private float levelStartingYLevel;

    [Header("Audio")]
    [SerializeField] private AudioMixer mixer;
    const string MIXER_MUSIC = "MusicVolume";
    const string MIXER_SFX = "SFXVolume";
    const string MIXER_MASTER = "MasterVolume";
    
    private void Start()
    {
        SetupLevels();
    }

    private void SetupLevels()
    {
        for (int i = 0; i < LevelManager.instance.getLevelCount(); i++)
        {
            GameObject levelButton = Instantiate(levelButtonPrefab, levelSelector.transform);
            SetupLevel(i, levelButton);
        }
    }

    private void SetupLevel(int level, GameObject levelButton)
    {
        levelButton.GetComponent<RectTransform>().anchoredPosition =
            new Vector2(
                level%levelsPerRow * levelButtonPadding - levelsPerRow * levelButtonPadding / 2,
                levelStartingYLevel - Mathf.Floor(level / levelsPerRow) * levelButtonPadding
            );
        levelButton.GetComponent<Button>().onClick.AddListener(() => SelectLevel(level));
        levelButton.GetComponentInChildren<TextMeshProUGUI>().text = (level + 1).ToString();
        if (!LevelManager.instance.isUnlocked(level))
        {
            levelButton.GetComponent<Button>().interactable = false;
            levelButton.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
        }
    }

    public void PlayGame()
    {
        LevelManager.instance.ContinueGame();
    }

    public void OpenSettings()
    {
        startingScreen.SetActive(false);
        settings.SetActive(true);
    }

    public void CloseSettings()
    {
        startingScreen.SetActive(true);
        settings.SetActive(false);
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

    public void OpenLevelSelector()
    {
        startingScreen.SetActive(false);
        levelSelector.SetActive(true);
    }

    public void CloseLevelSelector()
    {
        startingScreen.SetActive(true);
        levelSelector.SetActive(false);
    }

    public void SelectLevel(int level)
    {
        LevelManager.instance.LoadLevel(level);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

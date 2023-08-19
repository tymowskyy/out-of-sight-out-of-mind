using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject startingScreen;
    [SerializeField] private GameObject settings;
    [SerializeField] private GameObject levelSellector;

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

    public void UpdateVolume(float volume)
    {
        MusicManager.instance.UpdateVolume(volume);
    }

}

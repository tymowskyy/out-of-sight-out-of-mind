using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance { get; private set; }
    public bool paused { get; private set; }
    [SerializeField] private GameObject pauseMenu;

    private void Awake()
    {
        paused = false;
        instance = this;
    }

    public void OpenPauseMenu()
    {
        paused = true;

        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }

    public void ClosePauseMenu()
    {
        paused = false;

        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }

    public bool isPaused()
    {
        return paused;
    }

    public void ExitToMainMenu()
    {
        Time.timeScale = 1f;
        LevelManager.instance.LoadMainMenu();
    }
}

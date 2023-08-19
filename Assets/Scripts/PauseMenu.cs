using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance { get; private set; }
    public bool isPaused { get; private set; }
    [SerializeField] private GameObject pauseMenu;

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
}

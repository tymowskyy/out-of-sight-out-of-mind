using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
                level%levelsPerRow * levelButtonPadding - (levelsPerRow-1) * levelButtonPadding / 2,
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

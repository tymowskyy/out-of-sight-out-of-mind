using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
    private void Awake()
    {
        if(GameObject.FindGameObjectsWithTag("LevelManager").Length > 1)
        {
            if(instance != this)
                Destroy(gameObject);
        }
        else
        {
            if(PlayerPrefs.HasKey("lastLevelUnlocked"))
                lastLevelUnlocked = PlayerPrefs.GetInt("lastLevelUnlocked");
            else
            {
                PlayerPrefs.SetInt("lastLevelUnlocked", 0);
                PlayerPrefs.Save();
            }

            instance = this;
            currentLevel = 0;

            DontDestroyOnLoad(gameObject);
        }
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene(levelSceneNames[currentLevel]);
    }

    public void LoadLevel(int level)
    {
        currentLevel = level;
        SceneManager.LoadScene(levelSceneNames[level]);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(mainMenu);
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene(credits);
    }

    public void LoadBackrooms()
    {
        SceneManager.LoadScene("SEX");
    }

    public void LoadNextLevel()
    {
        if(currentLevel == levelSceneNames.Length - 1)
        {
            lastLevelUnlocked = currentLevel + 1;
            SceneManager.LoadScene(credits);
            return;
        }

        SceneManager.LoadScene(levelSceneNames[currentLevel+1]);
        currentLevel++;
        if (currentLevel > lastLevelUnlocked)
        {
            lastLevelUnlocked = currentLevel;
            PlayerPrefs.SetInt("lastLevelUnlocked", lastLevelUnlocked);
            PlayerPrefs.Save();
        }
    }

    public void RestartLevel()
    {
        int activeSceneId = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(activeSceneId);
    }

    public int getCurrentLevelId()
    {
        return currentLevel;
    }

    public int getLevelCount()
    {
        return levelSceneNames.Length;
    }

    public bool isUnlocked(int level)
    {
        return debugMode || level <= lastLevelUnlocked;
    }

    public static LevelManager instance { get; private set; }

    private int currentLevel;
    private int lastLevelUnlocked;

    [SerializeField] private string[] levelSceneNames;
    [SerializeField] private string mainMenu;
    [SerializeField] private string credits;
    [SerializeField] private bool debugMode;
}

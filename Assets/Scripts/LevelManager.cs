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
            currentLevel = lastLevelUnlocked;


            if (lastLevelUnlocked >= levelSceneNames.Length-1)
            {
                currentLevel = 0;
            }

            DontDestroyOnLoad(gameObject);
        }
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene(levelSceneNames[currentLevel]);

        DiscordRPC.instance.setStatus("In level " + (currentLevel + 1).ToString());
    }

    public void LoadLevel(int level)
    {
        currentLevel = level;
        SceneManager.LoadScene(levelSceneNames[level]);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(mainMenu);

        DiscordRPC.instance.setStatus("In the main menu");
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene(credits);

        DiscordRPC.instance.setStatus("Watching the credits");
    }

    public void LoadBackrooms()
    {
        SceneManager.LoadScene("SEX");

        DiscordRPC.instance.setStatus(":3");
    }

    public void LoadNextLevel()
    {
        if (currentLevel+1 > lastLevelUnlocked)
        {
            PlayerPrefs.SetInt("lastLevelUnlocked", currentLevel+1);
            PlayerPrefs.Save();
            lastLevelUnlocked = currentLevel+1;
        }


        if (currentLevel == levelSceneNames.Length - 1)
        {
            lastLevelUnlocked = currentLevel + 1;
            LoadCredits();

            currentLevel = 0;

            return;
        }

        SceneManager.LoadScene(levelSceneNames[currentLevel+1]);
        currentLevel++;

        DiscordRPC.instance.setStatus("In level " + (currentLevel+1).ToString());
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

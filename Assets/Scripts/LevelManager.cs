using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
    private void Awake()
    {
        if(GameObject.FindGameObjectsWithTag("LevelManager").Length > 1)
        {
            Destroy(gameObject);
        }

        instance = this;
        currentLevel = 0;

        DontDestroyOnLoad(gameObject);
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene(levelSceneNames[currentLevel]);
    }

    public void LoadNextLevel()
    {
        if(currentLevel == levelSceneNames.Length - 1)
        {
            currentLevel = 0;
            SceneManager.LoadScene(mainMenu);
            return;
        }

        SceneManager.LoadScene(levelSceneNames[currentLevel+1]);
        currentLevel++;
    }

    public void RestartLevel()
    {
        int activeSceneId = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(activeSceneId);
    }

    public static LevelManager instance { get; private set; }

    private int currentLevel;

    [SerializeField] private string[] levelSceneNames;
    [SerializeField] private string mainMenu;
}

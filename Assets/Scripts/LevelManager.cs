using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
    private void Awake()
    {
        instance = this;
        currentLevel = 0;

        DontDestroyOnLoad(gameObject);
    }

    public void LoadNextLevel()
    {
        if(currentLevel == levelSceneNames.Length - 1)
        {
            //we've finished the last level
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
}

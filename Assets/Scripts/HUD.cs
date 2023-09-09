using TMPro;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class HUD : MonoBehaviour
{
    private void Awake()
    {
        instance = this;

        int currentLevelId = LevelManager.instance.getCurrentLevelId()+1;

        if(currentLevelId < 10)
        {
            levelText.text = "0" + currentLevelId.ToString();
        } else
        {
            levelText.text = currentLevelId.ToString();
        }
    }

    public void setTutorialText(string text)
    {
        tutorialText.text = text;
    }

    public void setTutorialTextEnabled(bool _enabled)
    {
        tutorialText.gameObject.SetActive(_enabled);
    }

    public static HUD instance { get; private set; }

    [SerializeField] private TextMeshProUGUI tutorialText;
    [SerializeField] private TextMeshProUGUI levelText;
}

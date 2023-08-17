using TMPro;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class HUD : MonoBehaviour
{
    private void Awake()
    {
        instance = this;
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
}

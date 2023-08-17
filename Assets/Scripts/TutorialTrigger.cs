using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    private void Awake()
    {
        wasViewed = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && (!wasViewed || !displayOnlyOnce))
        {
            HUD.instance.setTutorialText(tutorialText);
            HUD.instance.setTutorialTextEnabled(true);

            wasViewed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            HUD.instance.setTutorialTextEnabled(false);
        }
    }

    private bool wasViewed;

    [SerializeField] private string tutorialText;
    [SerializeField] private bool displayOnlyOnce;
}

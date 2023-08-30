using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    private void Awake()
    {
        wasViewed = false;

        tutorialNotificationSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && (!wasViewed || !displayOnlyOnce))
        {
            HUD.instance.setTutorialText(tutorialText);
            HUD.instance.setTutorialTextEnabled(true);

            tutorialNotificationSound.Play();

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

    private AudioSource tutorialNotificationSound;

    private bool wasViewed;

    [SerializeField] private string tutorialText;
    [SerializeField] private bool displayOnlyOnce;
}

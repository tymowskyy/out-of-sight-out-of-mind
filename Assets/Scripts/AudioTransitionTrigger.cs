using UnityEngine;

public class AudioTransitionTrigger : MonoBehaviour
{
    private void transitionMusic()
    {
        MusicManager.instance.transitionMusic(newMusicClip, transitionDuration);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !reverseTrigger)
        {
            transitionMusic();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && reverseTrigger)
        {
            transitionMusic();
        }
    }

    [SerializeField] private AudioClip newMusicClip;
    [SerializeField] private float transitionDuration;
    [SerializeField] private bool reverseTrigger;
}

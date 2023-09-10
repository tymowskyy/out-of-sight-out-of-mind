using UnityEngine;

public class AudioTransitionTrigger : MonoBehaviour
{
    private void transitionMusic()
    {
        if(MusicManager.instance.getCurrentAudioClipName() != newMusicClip.name)
            MusicManager.instance.transitionMusic(newMusicClip, transitionDuration);

        gameObject.SetActive(false);
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

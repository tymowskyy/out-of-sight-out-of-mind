using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class VolumeSlider : MonoBehaviour
{
    [SerializeField] string mixerGroup;
    private Slider slider;
    private MusicManager musicManager;

    public void Awake()
    {
        musicManager = MusicManager.instance;
        slider = GetComponent<Slider>();
        slider.value = musicManager.GetVolume(mixerGroup);
        slider.onValueChanged.AddListener(UpdateVolume);
    }

    public void UpdateVolume(float volume)
    {
        musicManager.UpdateVolume(mixerGroup, volume);
    }
}

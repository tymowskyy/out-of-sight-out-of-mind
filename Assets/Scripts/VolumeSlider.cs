using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(UnityEngine.UI.Slider))]
public class VolumeSlider : MonoBehaviour
{
    [SerializeField] string mixerGroup;
    private UnityEngine.UI.Slider slider;
    private MusicManager musicManager;

    public void Awake()
    {
        musicManager = MusicManager.instance;
        slider = GetComponent<UnityEngine.UI.Slider>();
        slider.value = musicManager.GetVolume(mixerGroup);
        slider.onValueChanged.AddListener(UpdateVolume);
    }

    public void UpdateVolume(float volume)
    {
        musicManager.UpdateVolume(mixerGroup, volume);
    }
}

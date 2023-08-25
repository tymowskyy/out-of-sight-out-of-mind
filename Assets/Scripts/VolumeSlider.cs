using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(UnityEngine.UI.Slider))]
public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] string mixer_group;
    private UnityEngine.UI.Slider slider;

    public void Awake()
    {
        slider = GetComponent<UnityEngine.UI.Slider>();
        float volume;
        mixer.GetFloat(mixer_group, out volume);
        slider.value = Mathf.Pow(10, volume / 20);
        slider.onValueChanged.AddListener(UpdateVolume);
    }

    public void UpdateVolume(float volume)
    {
        mixer.SetFloat(mixer_group, Mathf.Log10(volume) * 20);
    }
}

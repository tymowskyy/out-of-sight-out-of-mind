using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class LightSwitch : MonoBehaviour
{
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        collider2d = GetComponent<Collider2D>();

        foreach(LightSource lightSource in lightSources)
        {
            setLightBulbTexture(lightSource);
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (collider2d.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition))) {
                toggle();
            }
        }
    }

    public void toggle()
    {
        audioSource.Play();

        foreach(LightSource lightSource in lightSources)
        {
            lightSource.toggle();

            setLightBulbTexture(lightSource);
        }
    }

    private void setLightBulbTexture(LightSource lightSource)
    {
        if (lightSource.isOn())
        {
            lightSource.lightBulbTexture.sprite = onTexture;
        }
        else
        {
            lightSource.lightBulbTexture.sprite = offTexture;
        }
    }

    private AudioSource audioSource;
    private Collider2D collider2d;

    [SerializeField] private LightSource[] lightSources;
    [SerializeField] private Sprite onTexture;
    [SerializeField] private Sprite offTexture;
}

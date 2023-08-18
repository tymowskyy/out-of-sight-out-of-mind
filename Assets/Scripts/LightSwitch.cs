using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class LightSwitch : MonoBehaviour
{
    private void Start()
    {
        foreach(LightSource lightSource in lightSources)
        {
            setLightBulbTexture(lightSource);
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (GetComponent<Collider2D>().OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition))) {
                toggle();
            }
        }
    }

    public void toggle()
    {
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

    [SerializeField] private LightSource[] lightSources;
    [SerializeField] private Sprite onTexture;
    [SerializeField] private Sprite offTexture;
}

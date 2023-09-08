using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSourceTrigger : MonoBehaviour
{

    private void Awake()
    {
        wasToggled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(wasToggled && toggleOnlyOnce)
        {
            return;
        }

        if(collision.CompareTag("Player"))
        {
            foreach(LightSource lightSource in lightSources)
            {
                lightSource.toggle();

                wasToggled = true;
            }
        }
    }

    private bool wasToggled;

    [SerializeField] private LightSource[] lightSources;
    [SerializeField] private bool toggleOnlyOnce;
}

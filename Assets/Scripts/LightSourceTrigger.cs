using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSourceTrigger : MonoBehaviour
{

    private void Awake()
    {
        wasToggled = false;
    }

    private void onTrigger() 
    {
        if (shouldToggle())
        {
            foreach (LightSource lightSource in lightSources)
            {
                lightSource.toggle();

                wasToggled = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !reverseToggle)
        {
            onTrigger();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && reverseToggle)
        {
            onTrigger();
        }
    }

    private bool shouldToggle()
    {
        return !wasToggled || !toggleOnlyOnce;
    }

    private bool wasToggled;

    [SerializeField] private LightSource[] lightSources;
    [SerializeField] private bool toggleOnlyOnce;
    [SerializeField] private bool reverseToggle;
}

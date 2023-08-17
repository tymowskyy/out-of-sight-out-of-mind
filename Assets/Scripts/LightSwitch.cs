using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class LightSwitch : MonoBehaviour
{
    [SerializeField] private LightSource[] lightSources;

    void Update()
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
        }
    }
}

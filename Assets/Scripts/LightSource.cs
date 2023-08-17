using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Light2D))]
public class LightSource : MonoBehaviour
{
    [SerializeField] private bool defaultOn;

    private bool isOn;
    private Collider2D colliderComp;
    private Light2D lightComp;
    private SpriteRenderer rendererComp;

    void Awake()
    {
        colliderComp = GetComponent<Collider2D>();
        lightComp = GetComponent<Light2D>();
        rendererComp = GetComponent<SpriteRenderer>();
        isOn = defaultOn;
        colliderComp.enabled = isOn;
        lightComp.enabled = isOn;
        rendererComp.enabled = isOn;
    }

    public void toggle() {
        isOn = !isOn;
        colliderComp.enabled = isOn;
        lightComp.enabled = isOn;
        rendererComp.enabled = isOn;
    }
}

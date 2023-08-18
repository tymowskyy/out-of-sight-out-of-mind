using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Light2D))]
public class LightSource : MonoBehaviour
{
    void Awake()
    {
        colliderComp = GetComponent<Collider2D>();
        lightComp = GetComponent<Light2D>();
        on = defaultOn;
        colliderComp.enabled = on;
        lightComp.enabled = on;
    }

    public void toggle() {
        on = !on;
        colliderComp.enabled = on;
        lightComp.enabled = on;
    }

    public bool isOn()
    {
        return on;
    }

    public SpriteRenderer lightBulbTexture;

    [SerializeField] private bool defaultOn;

    private bool on;
    private Collider2D colliderComp;
    private Light2D lightComp;
}

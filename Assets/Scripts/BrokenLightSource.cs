using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BrokenLightSource : MonoBehaviour
{
    private void Start()
    {
        toggleTimer = lightToggleTime;
        lightEnabled = startsEnabled;

        lightCollider = GetComponent<Collider2D>();
        lightSource = GetComponent<Light2D>();

        lightCollider.enabled = lightEnabled;
        lightSource.enabled = lightEnabled;
    }

    private void Update()
    {
        updateTimers();

        if(toggleTimer <= 0.0f)
        {
            lightEnabled = !lightEnabled;

            lightCollider.enabled = lightEnabled;
            lightSource.enabled = lightEnabled;

            toggleTimer = lightToggleTime;
        }
    }

    private void updateTimers()
    {
        toggleTimer -= Time.deltaTime;
    }

    private Collider2D lightCollider;
    private Light2D lightSource;

    private float toggleTimer;
    private bool lightEnabled;

    [SerializeField] private float lightToggleTime;
    [SerializeField] private bool startsEnabled;
}

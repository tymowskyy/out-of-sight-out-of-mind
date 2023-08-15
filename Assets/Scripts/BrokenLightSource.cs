using UnityEngine;
using UnityEngine.Rendering.Universal;

[System.Serializable]
public class Cycle
{
    public int repetitions;
    public float repDuration;
}

public class BrokenLightSource : MonoBehaviour
{
    private void Start()
    {
        if(cycles.Length == 0)
        {
            Debug.LogError("BrokenLightSource must have at least 1 cycle!");
            enabled = false;
            return;
        }

        toggleTimer = cycles[0].repDuration;
        lightEnabled = startsEnabled;

        lightCollider = GetComponent<Collider2D>();
        lightSource = GetComponent<Light2D>();

        lightCollider.enabled = lightEnabled;
        lightSource.enabled = lightEnabled;

        currentCycle = 0;
        currentRepetition = 1;
    }

    private void Update()
    {
        updateTimers();

        if(toggleTimer <= 0.0f)
        {
            currentRepetition++;

            if(currentRepetition > cycles[currentCycle].repetitions)
            {
                currentRepetition = 0;
                currentCycle++;

                if(currentCycle >= cycles.Length)
                {
                    currentCycle = 0;
                }
            }

            lightEnabled = !lightEnabled;

            lightCollider.enabled = lightEnabled;
            lightSource.enabled = lightEnabled;

            toggleTimer = cycles[currentCycle].repDuration;
        }
    }

    private void updateTimers()
    {
        toggleTimer -= Time.deltaTime;
    }

    private Collider2D lightCollider;
    private Light2D lightSource;

    private int currentCycle;
    private int currentRepetition;

    private float toggleTimer;
    private bool lightEnabled;

    [SerializeField] private Cycle[] cycles;
    [SerializeField] private bool startsEnabled;
}

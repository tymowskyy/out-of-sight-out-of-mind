using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BrokenLightSwitch : MonoBehaviour
{
    [System.Serializable]
    public class Cycle
    {
        public int repetitions;
        public float repDuration;
    }

    [SerializeField] private Cycle[] cycles;
    [SerializeField] private LightSource[] lightSources;

    private int currentCycle;
    private int currentRepetition;
    private float toggleTimer;

    private void Start()
    {
        if(cycles.Length == 0)
        {
            Debug.LogError("BrokenLightSource must have at least 1 cycle!");
            enabled = false;
            return;
        }

        toggleTimer = cycles[0].repDuration;
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

            toggleTimer = cycles[currentCycle].repDuration;

            foreach(LightSource lightSource in lightSources)
            {
                lightSource.toggle();
            }
        }
    }

    private void updateTimers()
    {
        toggleTimer -= Time.deltaTime;
    }
}
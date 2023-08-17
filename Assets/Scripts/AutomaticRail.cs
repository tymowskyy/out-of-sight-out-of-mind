using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticRail : MonoBehaviour
{
    public Transform railStart;
    public Transform railEnd;
    public GameObject head;
    public float frequency = 1f;
    public float offset = 0f;
    private float time = 0f;

    private void Start()
    {
        time = offset * (1f / frequency);
    }

    private void Update()
    {
        time += Time.deltaTime;
        time = time % (1f / frequency);
        float t = time * frequency; 
        if (t > 0.5f) {
            t = 1f - t;
        }
        t *= 2f; 
        head.transform.position = Vector3.Lerp(railStart.position, railEnd.position, t);
    }
}

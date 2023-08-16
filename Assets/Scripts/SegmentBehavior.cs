using UnityEngine;

public class SegmentBehavior : MonoBehaviour
{
    private void Awake()
    {
        lightSourceCount = 0;

        segmentCollider = GetComponent<Collider2D>();

        segmentCollider.isTrigger = true;
        collidingWithPlayer = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collidingWithPlayer = true;
            segmentCollider.isTrigger = true;
        }

        else if (collision.CompareTag("LightSource") && !collidingWithPlayer)
        {
            lightSourceCount++;
            segmentCollider.isTrigger = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collidingWithPlayer = false;
        }

        if (collision.CompareTag("LightSource"))
        {
            lightSourceCount--;

            if(lightSourceCount > 0)
                Debug.Log(lightSourceCount);

            if (lightSourceCount == 0)
            {
                segmentCollider.isTrigger = true;
            }
        }
    }

    bool collidingWithPlayer;

    private int lightSourceCount; //in how mant light sources are we
    private Collider2D segmentCollider;
}
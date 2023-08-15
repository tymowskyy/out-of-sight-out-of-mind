using UnityEngine;

public class SegmentBehavior : MonoBehaviour
{
    private void Awake()
    {
        lightSourceCount = 0;

        segmentCollider = GetComponent<Collider2D>();
        segmentationManager = transform.parent.GetComponent<ColliderSegmentation>();

        segmentCollider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            segmentationManager.OnSegmentTriggerEnterWithPlayer();
        }

        else if (collision.CompareTag("LightSource") && !segmentationManager.isCollidingWithPlayer())
        {
            lightSourceCount++;
            segmentCollider.isTrigger = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            segmentationManager.OnSegmentTriggerExitWithPlayer();
        }

        if (collision.CompareTag("LightSource"))
        {
            lightSourceCount--;

            if (lightSourceCount == 0)
            {
                segmentCollider.isTrigger = true;
            }
        }
    }

    private ColliderSegmentation segmentationManager;

    private int lightSourceCount; //in how mant light sources are we
    private Collider2D segmentCollider;
}
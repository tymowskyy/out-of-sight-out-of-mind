using UnityEngine;

public class SegmentBehavior : MonoBehaviour
{
    private bool[] isCollidingWithPlayerOffset = new bool[16];
    private PlayerStickingController stickingController;
    private void Awake()
    {
        stickingController = GameObject.FindWithTag("Player").GetComponent<PlayerStickingController>();
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
        }

        else if (collision.CompareTag("LightSource"))
        {
            lightSourceCount++;
            if (lightSourceCount == 1)
            {
                segmentCollider.isTrigger = false;
                for(int i=0; i<isCollidingWithPlayerOffset.Length; ++i)
                {
                    if(isCollidingWithPlayerOffset[i])
                    {
                        stickingController.colliderSegmentCount[i]++;
                    }
                }
            }
            if(collidingWithPlayer)
            {
                stickingController.OnPlayerStick();
            }
        }

        else if (collision.CompareTag("PlayerColliderOffset"))
        {
            int colliderIndex = collision.gameObject.GetComponent<PlayerColliderOffset>().index;
            isCollidingWithPlayerOffset[colliderIndex] = true;
            if (!segmentCollider.isTrigger)
                stickingController.colliderSegmentCount[colliderIndex]++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collidingWithPlayer = false;
        }

        else if (collision.CompareTag("LightSource"))
        {
            lightSourceCount--;

            if (lightSourceCount == 0)
            {
                segmentCollider.isTrigger = true;
                for(int i=0; i<isCollidingWithPlayerOffset.Length; ++i)
                {
                    if(isCollidingWithPlayerOffset[i])
                    {
                        stickingController.colliderSegmentCount[i]--;
                    }
                }      
            }
        }
        else if (collision.CompareTag("PlayerColliderOffset"))
        {
            int colliderIndex = collision.gameObject.GetComponent<PlayerColliderOffset>().index;
            isCollidingWithPlayerOffset[colliderIndex] = false; 
            if (!segmentCollider.isTrigger)
                stickingController.colliderSegmentCount[colliderIndex]--;
        }
    }

    bool collidingWithPlayer;

    private int lightSourceCount; //in how mant light sources are we
    private Collider2D segmentCollider;
}
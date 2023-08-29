using System.Collections.Generic;
using UnityEngine;

public class SegmentBehavior : MonoBehaviour
{
    private List<bool> isCollidingWithPlayerOffset = new List<bool>();
    private PlayerStickingController stickingController;
    private void Awake()
    {
        GameObject player = GameObject.FindWithTag("Player");
        GameObject playerFeet = GameObject.FindWithTag("PlayerFeet");

        stickingController = player.GetComponent<PlayerStickingController>();
        playerFeetCO = playerFeet.GetComponent<PlayerColliderOffset>();

        for (int i=0; i<stickingController.colliderOffsets.Length; ++i)
            isCollidingWithPlayerOffset.Add(false);

        lightSourceCount = 0;

        segmentCollider = GetComponent<Collider2D>();

        segmentCollider.isTrigger = true;
        collidingWithPlayer = false;
        collidingWithPlayerFeet = false;
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
                for(int i=0; i<isCollidingWithPlayerOffset.Count; ++i)
                {
                    if(isCollidingWithPlayerOffset[i])
                    {
                        stickingController.colliderSegmentCount[i]++;
                    }
                }
                if(collidingWithPlayer)
                {
                    stickingController.OnPlayerStick();
                }
                if(collidingWithPlayerFeet)
                {
                    playerFeetCO.index++;
                }
            }
        }

        else if (collision.CompareTag("PlayerColliderOffset"))
        {
            int colliderIndex = collision.gameObject.GetComponent<PlayerColliderOffset>().index;
            isCollidingWithPlayerOffset[colliderIndex] = true;
            if (!segmentCollider.isTrigger)
                stickingController.colliderSegmentCount[colliderIndex]++;
        }

        if (collision.CompareTag("PlayerFeet"))
        {
            collidingWithPlayerFeet = true;
            if(!segmentCollider.isTrigger)
                playerFeetCO.index++;
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
                for(int i=0; i<isCollidingWithPlayerOffset.Count; ++i)
                {
                    if(isCollidingWithPlayerOffset[i])
                    {
                        stickingController.colliderSegmentCount[i]--;
                    }
                }
                
                if(collidingWithPlayerFeet)
                {
                    playerFeetCO.index--;
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

        else if(collision.CompareTag("PlayerFeet"))
        {
            collidingWithPlayerFeet = false;
            if (!segmentCollider.isTrigger)
            {
                playerFeetCO.index--;
            }
        }
    }

    private PlayerColliderOffset playerFeetCO;

    private bool collidingWithPlayer;
    private bool collidingWithPlayerFeet;

    private int lightSourceCount; //in how mant light sources are we
    private Collider2D segmentCollider;
}
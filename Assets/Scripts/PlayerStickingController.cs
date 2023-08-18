using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStickingController : MonoBehaviour
{
    public PlayerColliderOffset[] colliderOffsets;
    public int[] colliderSegmentCount = new int[24];
    private bool isStuck = false;

    void Awake()
    {
        for (int i=0; i<colliderOffsets.Length; ++i)
        {
            colliderOffsets[i].index = i;
        }
    }

    void Update()
    {
        if (isStuck) {
            UnstickPlayer();
            isStuck = false;
        }
    }

    public void UnstickPlayer()
    {
        for (int i=0; i<colliderOffsets.Length; ++i)
        {
            if (colliderSegmentCount[i] == 0)
            {
                transform.position = colliderOffsets[i].transform.position;
                return;
            }
        }
        Debug.Break();
        //LevelManager.instance.RestartLevel();
    }

    public void OnPlayerStick()
    {
        isStuck = true;
    }
}

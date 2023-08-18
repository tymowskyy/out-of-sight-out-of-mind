using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStickingController : MonoBehaviour
{
    public PlayerColliderOffset[] colliderOffsets;
    public List<int> colliderSegmentCount = new List<int>();
    private bool isStuck = false;

    void Awake()
    {
        colliderSegmentCount = new List<int>(colliderOffsets.Length);
        for (int i=0; i<colliderOffsets.Length; ++i)
        {
            colliderSegmentCount.Add(0);
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

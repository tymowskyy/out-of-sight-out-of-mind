using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStickingController : MonoBehaviour
{
    void Awake()
    {
        playerController = GetComponent<PlayerController>();

        colliderSegmentCount = new List<int>(colliderOffsets.Length);
        for (int i=0; i<colliderOffsets.Length; ++i)
        {
            colliderSegmentCount.Add(0);
            colliderOffsets[i].index = i;
        }
    }

    void FixedUpdate()
    {
        if (isStuck) {
            if (enableUnsticking)
            {
                UnstickPlayer();
            }
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

        if (playerController.enabled)
        {
            StartCoroutine(playerController.die(beforeDeathTimeout));
        }
    }

    public void OnPlayerStick()
    {
        isStuck = true;
        playerController.isStuck = isStuck;
    }

    [SerializeField] private float beforeDeathTimeout;


    public bool enableUnsticking = true;
    public PlayerColliderOffset[] colliderOffsets;
    public  List<int> colliderSegmentCount = new List<int>();

    private PlayerController playerController;
    private bool isStuck = false;
}

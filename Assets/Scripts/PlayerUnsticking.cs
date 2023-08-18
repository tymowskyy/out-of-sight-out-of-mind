using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlayerUnsticking : MonoBehaviour
{
    private Transform playerTransform;
    private Collider2D colliderComp;
    [SerializeField] private Transform topLeftCorner;
    [SerializeField] private Transform bottomRightCorner;
    [SerializeField] private float maxUnstickDistance;
    public void Start() {
        playerTransform = GameObject.FindWithTag("Player").transform;
        colliderComp = GetComponent<Collider2D>();
    }

    public void OnPlayerStick() {
        Vector3 playerPos = playerTransform.position;
        if (colliderComp.OverlapPoint(playerPos)) {
            float distXp, distXm, distYp, distYm;
            distXp = bottomRightCorner.position.x - playerPos.x;
            distXm = playerPos.x - topLeftCorner.position.x;
            distYp = topLeftCorner.position.y - playerPos.y;
            distYm = playerPos.y - bottomRightCorner.position.y;

            float mini = new float[] {distXp, distXm, distYp, distYm}.Min();

            if (mini < maxUnstickDistance) {
                if(mini == distXp)
                    playerPos.x = bottomRightCorner.position.x + playerTransform.localScale.x/2;
                else if(mini == distXm)
                    playerPos.x = topLeftCorner.position.x - playerTransform.localScale.x/2;
                else if(mini == distYp)
                    playerPos.y = topLeftCorner.position.y + playerTransform.localScale.y/2;
                else if(mini == distYm)
                    playerPos.y = bottomRightCorner.position.y - playerTransform.localScale.y/2;
                playerTransform.position = playerPos;
            }
            else {
                LevelManager.instance.RestartLevel();
            }
        }
        else {
            if(playerPos.x > bottomRightCorner.position.x)
                playerPos.x = bottomRightCorner.position.x + playerTransform.localScale.x/2;
            else if(playerPos.x < topLeftCorner.position.x)
                playerPos.x = topLeftCorner.position.x - playerTransform.localScale.x/2;
            else if(playerPos.y > topLeftCorner.position.y)
                playerPos.y = topLeftCorner.position.y + playerTransform.localScale.y/2;
            else if(playerPos.y < bottomRightCorner.position.y)
                playerPos.y = bottomRightCorner.position.y - playerTransform.localScale.y/2;
            playerTransform.position = playerPos;
        }
    }
}

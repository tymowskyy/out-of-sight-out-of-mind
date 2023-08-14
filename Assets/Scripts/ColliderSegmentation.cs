using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderSegmentation : MonoBehaviour
{
    private void Start()
    {
        float platformWidth = bottomRightCorner.position.x - topLeftCorner.position.x;
        float platformHeight = topLeftCorner.position.y - bottomRightCorner.position.y;

        float segmentWidth = platformWidth / numberOfSegmentsX;
        float segmentHeight = platformHeight / numberOfSegmentsY;

        for(int xIdx = 0; xIdx < numberOfSegmentsX; xIdx++)
        {
            for(int yIdx = 0; yIdx < numberOfSegmentsY; yIdx++)
            {
                float segmentPosX = topLeftCorner.position.x + xIdx * segmentWidth;
                float segmentPosY = topLeftCorner.position.y - yIdx * segmentHeight;

                GameObject segment = Instantiate(segmentPrefab, transform);
                segment.transform.position = new Vector3(segmentPosX + segmentWidth / 2, segmentPosY - segmentHeight / 2);
                segment.transform.localScale = new Vector3(segmentWidth / transform.localScale.x, segmentHeight / transform.localScale.y);
            }
        }
    }

    [SerializeField] private GameObject segmentPrefab;

    [SerializeField] private Transform topLeftCorner;
    [SerializeField] private Transform bottomRightCorner;

    [SerializeField] private int numberOfSegmentsX;
    [SerializeField] private int numberOfSegmentsY;
}

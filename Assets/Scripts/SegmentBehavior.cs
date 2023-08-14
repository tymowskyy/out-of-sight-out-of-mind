using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentBehavior : MonoBehaviour
{
    private void Awake()
    {
        segmentCollider = GetComponent<Collider2D>();

        segmentCollider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("LightSource"))
        {
            segmentCollider.isTrigger = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("LightSource"))
        {
            segmentCollider.isTrigger = true;
        }
    }

    private Collider2D segmentCollider;

}
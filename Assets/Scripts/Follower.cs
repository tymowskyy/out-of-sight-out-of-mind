using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Config")]
    [SerializeField] private Transform player;
    [SerializeField] private float offsetZ;
    [SerializeField] private Vector2 minimumPosition;
    [SerializeField] private Vector2 maximalPosition;
    void Start()
    {
    }

    void Update () 
    {
        transform.position = new Vector3 (
            Mathf.Min(Mathf.Max(player.position.x,minimumPosition.x),maximalPosition.x),
            Mathf.Min(Mathf.Max(player.position.y,minimumPosition.y),maximalPosition.y),
            offsetZ); // Camera follows the player with specified offset position
    }
}

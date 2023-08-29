using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceDoor : MonoBehaviour
{
    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        player = GameObject.FindGameObjectWithTag("Player");

        player.transform.position = doorCenter.position;
    }

    private void Start()
    {
        player.GetComponent<PlayerController>().enabled = false;
        player.GetComponent<Rigidbody2D>().gravityScale = 0f;
        animator.enabled = true;
    }

    public void onOpenAnimationEnd()
    {
        player.GetComponent<PlayerController>().enabled = true;
        player.GetComponent<Rigidbody2D>().gravityScale = 1f;
        animator.enabled = false;

        //set sorting layer to the default one
        spriteRenderer.sortingLayerName ="Default";
    }

    private Animator animator;
    private GameObject player;

    private SpriteRenderer spriteRenderer;

    [SerializeField] private Transform doorCenter;
}

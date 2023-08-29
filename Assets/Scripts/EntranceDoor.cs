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

        animator.enabled = true;
    }

    public void onOpenAnimationEnd()
    {
        animator.enabled = false;

        player.GetComponent<PlayerController>().enabled = true;

        //set sorting layer to the default one
        spriteRenderer.sortingLayerName ="Default";
    }

    private Animator animator;
    private GameObject player;

    private SpriteRenderer spriteRenderer;

    [SerializeField] private Transform doorCenter;
}

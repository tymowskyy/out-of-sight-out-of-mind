using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceDoor : MonoBehaviour
{
    private void Awake()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");

        player.transform.position = doorCenter.position;
    }

    private void Start()
    {
        player.GetComponent<Movement>().enabled = false;

        animator.enabled = true;
    }

    public void onOpenAnimationEnd()
    {
        animator.enabled = false;

        player.GetComponent<Movement>().enabled = true;
    }

    private Animator animator;
    private GameObject player;

    [SerializeField] private Transform doorCenter;
}

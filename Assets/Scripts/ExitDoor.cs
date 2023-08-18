using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    private void Awake()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void onCloseAnimationEnd()
    {
        if(shouldLoadNextLevel)
        {
            LevelManager.instance.LoadNextLevel();
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Player")) {
            shouldLoadNextLevel = true;
            animator.enabled = true;

            player.GetComponent<Movement>().enabled = false;
            player.GetComponent<PlayerPickupManager>().enabled = false;
            player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

            player.transform.position = doorCenter.position;
        }
    }

    private bool shouldLoadNextLevel;

    private Animator animator;
    private GameObject player;

    [SerializeField] private Transform doorCenter;
}

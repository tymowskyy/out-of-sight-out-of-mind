using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

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
            spriteRenderer.sortingLayerName = "Door";

            shouldLoadNextLevel = true;
            animator.enabled = true;

            PlayerController playerController = player.GetComponent<PlayerController>();
            Animator playerAnimator = playerController.getAnimator();

            playerController.enabled = false;

            player.GetComponent<PlayerPickupManager>().enabled = false;
            player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

            player.transform.position = doorCenter.position;

            playerAnimator.SetFloat("velocityX", 0f);
            playerAnimator.SetFloat("velocityY", 0f);
            playerAnimator.ResetTrigger("onJump");
        }
    }

    private bool shouldLoadNextLevel;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private GameObject player;

    [SerializeField] private Transform doorCenter;
}

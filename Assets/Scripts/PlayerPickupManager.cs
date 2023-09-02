using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickupManager : MonoBehaviour
{
    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(InputManager.instance.GetKeyDown(KeyCode.G))
        {
            if(currentPickup != null)
                dropPickup();
        }
    }

    private void grabPickup(GameObject pickup)
    {
        pickup.transform.SetParent(transform);
        pickup.transform.position = handTransform.position;

        currentPickup = pickup.gameObject;

        Collider2D[] colliders = pickup.GetComponents<Collider2D>();

        foreach(Collider2D collider in colliders)
        {
            if(!collider.isTrigger)
            {
                pickupPhysicsCollider = collider;
                break;
            }
        }

        pickupRigidbody = pickup.GetComponent<Rigidbody2D>();
        pickupSpriteRenderer = pickup.GetComponent<SpriteRenderer>();

        pickupPhysicsCollider.enabled = false;

        pickupRigidbody.velocity = Vector3.zero;
        pickupRigidbody.bodyType = RigidbodyType2D.Kinematic;

        pickupSpriteRenderer.sortingLayerName = "Pickup";
    }

    private void dropPickup()
    {
        currentPickup.transform.SetParent(null);
        pickupPhysicsCollider.enabled = true;

        pickupRigidbody.bodyType = RigidbodyType2D.Dynamic;
        pickupRigidbody.AddForce(playerRigidbody.velocity, ForceMode2D.Impulse);

        pickupSpriteRenderer.sortingLayerName = "Default";

        currentPickup = null;
        pickupRigidbody = null;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Pickup") && currentPickup == null)
        {
            grabPickup(collision.gameObject);
        }
    }

    private GameObject currentPickup;
    private Collider2D pickupPhysicsCollider;
    private Rigidbody2D pickupRigidbody;
    private SpriteRenderer pickupSpriteRenderer;

    private Rigidbody2D playerRigidbody;

    [SerializeField] private Transform handTransform;
}

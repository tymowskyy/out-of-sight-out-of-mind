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
        if(Input.GetKeyDown(KeyCode.G))
        {
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

        pickupPhysicsCollider.enabled = false;
        pickupRigidbody.bodyType = RigidbodyType2D.Kinematic;
    }

    private void dropPickup()
    {
        currentPickup.transform.SetParent(null);
        pickupPhysicsCollider.enabled = true;

        pickupRigidbody.bodyType = RigidbodyType2D.Dynamic;
        pickupRigidbody.AddForce(playerRigidbody.velocity, ForceMode2D.Impulse);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Pickup"))
        {
            grabPickup(collision.gameObject);
        }
    }

    private GameObject currentPickup;
    private Collider2D pickupPhysicsCollider;
    private Rigidbody2D pickupRigidbody;

    private Rigidbody2D playerRigidbody;

    [SerializeField] private Transform handTransform;
}

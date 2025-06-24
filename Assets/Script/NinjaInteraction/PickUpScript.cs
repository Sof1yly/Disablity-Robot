using System.Collections.Generic;
using UnityEngine;

public class PickUpScript : MonoBehaviour
{
    [Header("Interaction Settings")]
    public Transform holdingPoint;
    public KeyCode pickupDropKey = KeyCode.E;
    private string pickableItemTag = "canPickUp";

    public static GameObject currentHeldItem = null;
    private Rigidbody heldItemRigidbody = null;
    private Transform originalItemParent = null;
    private bool wasKinematicBeforePickup = false;

    private List<GameObject> interactableItemsInTrigger = new List<GameObject>();

    public static event System.Action<GameObject> OnItemPickedUp;
    public static event System.Action<GameObject> OnDropAttempt;

    // Initializes the holding point for the item and checks if it's assigned
    void Start()
    {
        if (holdingPoint == null)
        {
            holdingPoint = this.transform;
            Debug.LogWarning("Holding Point not assigned for PickUpScript.");
        }
    }

    // Handles item pickup and drop logic when the designated key is pressed
    void Update()
    {
        if (Input.GetKeyDown(pickupDropKey))
        {
            if (currentHeldItem != null)
            {
                OnDropAttempt?.Invoke(currentHeldItem);
                if (currentHeldItem != null)
                {
                    DropItem();
                }
            }
            else if (interactableItemsInTrigger.Count > 0)
            {
                GameObject itemToPickUp = interactableItemsInTrigger[0];
                interactableItemsInTrigger.RemoveAt(0);
                PickupItem(itemToPickUp);
            }
        }
    }

    // Detects when a pickable item enters the trigger zone
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(pickableItemTag) &&
            !interactableItemsInTrigger.Contains(other.gameObject) &&
            other.gameObject != currentHeldItem)
        {
            interactableItemsInTrigger.Add(other.gameObject);
        }
    }

    // Detects when a pickable item exits the trigger zone
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(pickableItemTag))
        {
            if (interactableItemsInTrigger.Remove(other.gameObject))
            {
                Debug.Log($"Item exited trigger: {other.gameObject.name}. Total items in trigger: {interactableItemsInTrigger.Count}");
            }
        }
    }

    // Handles the logic for picking up a specified item
    private void PickupItem(GameObject item)
    {
        if (item == null) return;

        currentHeldItem = item;
        originalItemParent = currentHeldItem.transform.parent;

        heldItemRigidbody = currentHeldItem.GetComponent<Rigidbody>();

        if (heldItemRigidbody != null)
        {
            wasKinematicBeforePickup = heldItemRigidbody.isKinematic;
            heldItemRigidbody.isKinematic = true;
            heldItemRigidbody.useGravity = false;
        }

        currentHeldItem.transform.SetParent(holdingPoint);
        currentHeldItem.transform.localPosition = Vector3.zero;
        currentHeldItem.transform.localRotation = Quaternion.identity;
        currentHeldItem.transform.localScale = Vector3.one;

        OnItemPickedUp?.Invoke(currentHeldItem);
    }

    // Handles the logic for dropping the currently held item
    private void DropItem()
    {
        if (currentHeldItem == null) return;

        currentHeldItem.transform.SetParent(originalItemParent);

        if (heldItemRigidbody != null)
        {
            heldItemRigidbody.isKinematic = false;
            heldItemRigidbody.useGravity = true;
            heldItemRigidbody.linearVelocity = Vector3.zero;
            heldItemRigidbody.angularVelocity = Vector3.zero;
        }

        currentHeldItem = null;
        heldItemRigidbody = null;
        originalItemParent = null;
        wasKinematicBeforePickup = false;
    }

    // Releases the currently held item without resetting physics or parent (used for external handling like DropPoint)
    public GameObject ReleaseHeldItemWithoutPhysicsReset()
    {
        if (currentHeldItem == null) return null;

        GameObject releasedItem = currentHeldItem;

        currentHeldItem = null;
        heldItemRigidbody = null;
        originalItemParent = null;
        wasKinematicBeforePickup = false;

        return releasedItem;
    }
}

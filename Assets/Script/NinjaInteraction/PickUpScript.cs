using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script handles picking up and dropping single items within a trigger zone.
/// It has been updated to have a public static reference to the currently held item
/// for other scripts like DropPoint to access.
/// Attach this script to your player GameObject.
/// </summary>
public class PickUpScript : MonoBehaviour
{
    [Header("Interaction Settings")]
    [Tooltip("The Transform where the item will be held. Usually an empty child GameObject of the player.")]
    public Transform holdingPoint;

    [Tooltip("The KeyCode to press for picking up and dropping items.")]
    public KeyCode pickupDropKey = KeyCode.E;

    [Tooltip("The tag assigned to all pickable items in your scene.")]
    private string pickableItemTag = "canPickUp"; // Confirmed pickable item tag

    // --- Private Variables to manage item state ---
    // Make currentHeldItem public static so DropPoint can access it.
    public static GameObject currentHeldItem = null; // Stores the GameObject currently being held
    private Rigidbody heldItemRigidbody = null;      // Stores the Rigidbody of the held item (if it has one)
    private Transform originalItemParent = null;     // Stores the original parent of the item before pickup
    private bool wasKinematicBeforePickup = false;   // Stores the Rigidbody's kinematic state before pickup

    // A list to keep track of all interactable items currently within this object's trigger zone.
    // When the pickup key is pressed, the first item in this list will be picked up.
    private List<GameObject> interactableItemsInTrigger = new List<GameObject>();

    /// <summary>
    /// Event that is invoked when an item is successfully picked up.
    /// Subscribers can listen to this event to react to item pickups.
    /// The GameObject passed is the item that was just picked up.
    /// </summary>
    public static event System.Action<GameObject> OnItemPickedUp;

    /// <summary>
    /// NEW EVENT: Event that is invoked when the player attempts to drop an item.
    /// Subscribers (like DropPoint) can listen to this event to intercept the drop.
    /// The GameObject passed is the item currently being held.
    /// </summary>
    public static event System.Action<GameObject> OnDropAttempt;

    /// <summary>
    /// Called when the script instance is being loaded.
    /// </summary>
    void Start()
    {
        // If no specific holding point is assigned in the Inspector,
        // use this GameObject's (the player's) transform as a fallback.
        // It's recommended to create a dedicated child GameObject for precise placement.
        if (holdingPoint == null)
        {
            holdingPoint = this.transform;
            Debug.LogWarning("Holding Point not assigned for PickUpScript. " +
                             "Using this GameObject's transform as holding point. " +
                             "Consider assigning a dedicated child Transform for better visual placement.");
        }
    }

    /// <summary>
    /// Called once per frame. Handles input for picking up/dropping items.
    /// </summary>
    void Update()
    {
        // Check if the defined pickup/drop key is pressed down during this frame.
        if (Input.GetKeyDown(pickupDropKey))
        {
            // --- Scenario 1: An item is currently being held ---
            if (currentHeldItem != null)
            {
                // --- Observer Pattern: Notify observers about the drop attempt. ---
                // An observer (like DropPoint) might handle the drop and nullify currentHeldItem.
                OnDropAttempt?.Invoke(currentHeldItem);

                // If currentHeldItem is still not null after observers have had a chance to react,
                // it means no observer handled the drop (e.g., player not in a DropPoint zone),
                // so proceed with a normal drop to the world.
                if (currentHeldItem != null)
                {
                    DropItem();
                }
            }
            // --- Scenario 2: No item is held, but there are items available in the trigger ---
            else if (interactableItemsInTrigger.Count > 0)
            {
                // Get the first item from the list of detected items to pick it up.
                GameObject itemToPickUp = interactableItemsInTrigger[0];
                // Remove the item from the list immediately, as it's now being picked up
                // and should no longer be considered "in the trigger" for interaction purposes.
                interactableItemsInTrigger.RemoveAt(0);

                PickupItem(itemToPickUp); // Call the method to handle the pickup logic.
            }
        }
    }

    /// <summary>
    /// Called when another collider enters this object's trigger collider.
    /// </summary>
    /// <param name="other">The Collider that entered the trigger.</param>
    private void OnTriggerEnter(Collider other)
    {
        // 1. Check if the entering collider has the specified pickable item tag.
        // 2. Check if this item is not already in our list (prevents duplicates).
        // 3. Check if this item is not the one we are currently holding (prevents re-adding self).
        if (other.CompareTag(pickableItemTag) &&
            !interactableItemsInTrigger.Contains(other.gameObject) &&
            other.gameObject != currentHeldItem)
        {
            interactableItemsInTrigger.Add(other.gameObject);
            Debug.Log($"Item entered trigger: {other.gameObject.name}. Total items in trigger: {interactableItemsInTrigger.Count}");
        }
    }

    /// <summary>
    /// Called when another collider exits this object's trigger collider.
    /// </summary>
    /// <param name="other">The Collider that exited the trigger.</param>
    private void OnTriggerExit(Collider other)
    {
        // If the exiting collider has the pickable item tag, remove it from our list.
        if (other.CompareTag(pickableItemTag))
        {
            if (interactableItemsInTrigger.Remove(other.gameObject))
            {
                Debug.Log($"Item exited trigger: {other.gameObject.name}. Total items in trigger: {interactableItemsInTrigger.Count}");
            }
        }
    }

    /// <summary>
    /// Handles the logic for picking up a specified item.
    /// </summary>
    /// <param name="item">The GameObject to be picked up.</param>
    private void PickupItem(GameObject item)
    {
        // Basic null check to prevent errors.
        if (item == null) return;

        // Assign the item to the current held item variable.
        currentHeldItem = item;

        // Store the item's original parent so we can restore it when dropped.
        originalItemParent = currentHeldItem.transform.parent;

        // Attempt to get the Rigidbody component of the item.
        // Items intended to be picked up and dropped often have a Rigidbody for physics.
        heldItemRigidbody = currentHeldItem.GetComponent<Rigidbody>();

        if (heldItemRigidbody != null)
        {
            // Store the current kinematic state. This is important if the item
            // was already kinematic (e.g., a decorative prop) and shouldn't
            // have physics re-enabled when dropped.
            wasKinematicBeforePickup = heldItemRigidbody.isKinematic;

            heldItemRigidbody.isKinematic = true;       // Disable physics simulation (it won't react to forces)
            heldItemRigidbody.useGravity = false;       // Disable gravity
        }

        // Parent the item to the holding point Transform.
        // This makes the item move along with the player and the holding point.
        currentHeldItem.transform.SetParent(holdingPoint);

        // Reset the item's local position and rotation relative to the holding point.
        // This makes it appear exactly at the holdingPoint's local position.
        currentHeldItem.transform.localPosition = Vector3.zero;
        currentHeldItem.transform.localRotation = Quaternion.identity;

        // Ensure the local scale is normal. Sometimes items might have weird scales
        // if parented differently. Vector3.one means (1,1,1) local scale.
        currentHeldItem.transform.localScale = Vector3.one;

        Debug.Log($"Successfully picked up item: {currentHeldItem.name}");

        // --- Observer Pattern: Invoke the event after a successful pickup ---
        // The '?' (null-conditional operator) ensures the event is only invoked if there are any subscribers.
        OnItemPickedUp?.Invoke(currentHeldItem);
    }

    /// <summary>
    /// Handles the logic for dropping the currently held item.
    /// This version simply releases the item into the world, allowing DropPoints
    /// to independently check for it.
    /// </summary>
    private void DropItem()
    {
        // Basic null check to prevent errors if nothing is held.
        if (currentHeldItem == null) return;

        // Reset the item's parent to its original parent (or null for world space).
        // This detaches it from the holding point.
        currentHeldItem.transform.SetParent(originalItemParent);

        // If the item had a Rigidbody, re-enable its physics properties.
        if (heldItemRigidbody != null)
        {
            // For a regular drop, assume pickable items should always respond to physics.
            // This overrides the 'wasKinematicBeforePickup' if the item became kinematic
            // due to a DropPoint, ensuring it falls when dropped normally.
            heldItemRigidbody.isKinematic = false;
            heldItemRigidbody.useGravity = true;
            heldItemRigidbody.linearVelocity = Vector3.zero;     // Stop any lingering linear velocity
            heldItemRigidbody.angularVelocity = Vector3.zero; // Stop any lingering angular velocity
        }

        Debug.Log($"Successfully dropped item: {currentHeldItem.name} (released into world)");

        // Clear references to the dropped item.
        currentHeldItem = null;
        heldItemRigidbody = null;
        originalItemParent = null;
        wasKinematicBeforePickup = false; // Reset for next pickup
    }

    /// <summary>
    /// Releases the currently held item without applying physics or resetting parent.
    /// Used when another script (like DropPoint) is taking immediate control of the item.
    /// This prevents a "double drop" or item briefly falling before snapping.
    /// </summary>
    /// <returns>The GameObject that was released, or null if no item was held.</returns>
    public GameObject ReleaseHeldItemWithoutPhysicsReset()
    {
        if (currentHeldItem == null) return null;

        GameObject releasedItem = currentHeldItem;

        // Clear references within PickUpScript to indicate it's no longer held.
        currentHeldItem = null;
        heldItemRigidbody = null;
        originalItemParent = null;
        wasKinematicBeforePickup = false; // Reset for next pickup

        Debug.Log($"PickUpScript released control of {releasedItem.name} for external handling by DropPoint.");
        return releasedItem;
    }
}

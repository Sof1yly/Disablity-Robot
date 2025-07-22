using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ItemPickup : MonoBehaviour
{
    public ItemSO itemData;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("where my collider fucker");
        var eq = other.GetComponent<EquipmentManager>();
        if (eq != null)
        {
            if(eq.HasFreeSlot())
            {
                eq.PickupItem(itemData);
                Destroy(gameObject); 
            }
            else
            {
                Debug.Log("slot full");
                return; 
            }
        }
    }
}

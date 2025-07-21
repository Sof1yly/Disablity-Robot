using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Collider))]
public class RndItemPickUp : MonoBehaviour
{
    public List<ItemSO> itemList = new List<ItemSO>();

    void OnTriggerEnter(Collider other)
    {
        var eq = other.GetComponent<EquipmentManager>();
        if(eq == null)
        {
            return;
        }

        if (!eq.HasFreeSlot()) 
        {
            Debug.Log("slot full");
            return;
        }
        if(itemList.Count == 0)
        {
            Debug.Log("forgot to put item in list");
            return;

        }

        int idx = Random.Range(0, itemList.Count);
        ItemSO picked = itemList[idx];

        eq.PickupItem(picked);
        Debug.Log($"Picked up item: {picked.name} ");

        Destroy(gameObject);
    }



}

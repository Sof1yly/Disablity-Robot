using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DavidJalbert;

[RequireComponent(typeof(Collider))]
public class RndItemPickUp : MonoBehaviour
{
    [SerializeField] AudioClip pickupSfx;
    [Header("Item Settings")]
    public List<ItemSO> itemList = new List<ItemSO>();


    public float respawnMinTime = 3f;
    public float respawnMaxTime = 5f;

    private Collider pickupCollider;
    private Renderer[] renderers;

    void Awake()
    {
        pickupCollider = GetComponent<Collider>();
        renderers = GetComponentsInChildren<Renderer>(true);
    }

    void OnTriggerEnter(Collider other)
    {
        var eq = other.GetComponent<EquipmentManager>();
        if (eq == null)
            return;

        if (!eq.HasFreeSlot())
        {
            Debug.Log("Slot full — can't pick up item");
            return;
        }

        if (itemList == null || itemList.Count == 0)
        {
            Debug.LogWarning("No items in itemList!");
            return;
        }

        // Pick a random item
        int idx = Random.Range(0, itemList.Count);
        ItemSO picked = itemList[idx];

        eq.PickupItem(picked);
        Debug.Log(other);
        SoundPlayer.Instance.PlaySound(pickupSfx, other.transform.parent.GetComponentInChildren<TinyCarAudio>().player);
        Debug.Log($"Picked up item: {picked.name}");

        StartCoroutine(RespawnRoutine());
    }

    IEnumerator RespawnRoutine()
    {

        if (pickupCollider != null)
        {
            pickupCollider.enabled = false;
        }

        foreach (var rend in renderers)
        {
            rend.enabled = false;
        }


        float delay = Random.Range(respawnMinTime, respawnMaxTime);
        yield return new WaitForSeconds(delay);


        if (pickupCollider != null)
        {
            pickupCollider.enabled = true;
        }
            
        foreach (var rend in renderers)
        {
            rend.enabled = true;
        }
    }
}

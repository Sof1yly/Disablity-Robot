using UnityEngine;
using UnityEngine.UI;
using System.Collections;  // Required for Coroutine

public class EquipmentManager : MonoBehaviour
{
    [Header("UI Slot Images")]
    public Image mainSlotImage;
    public Image supSlotImage;

    ItemSO mainSlot;
    ItemSO supSlot;

    private bool isCooldownActive = false;  
    private float cooldownTime = 4f; 

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton4)) //will change to input action later dont worry my nigga
        {
            Debug.Log("use item");
            UseMain();
        }

    }

    public bool HasFreeSlot()
    {
        return mainSlot == null || supSlot == null;
    }

    public void PickupItem(ItemSO item)
    {
        if (mainSlot == null)
        {
            mainSlot = item;
            mainSlotImage.sprite = item.icon;
            mainSlotImage.enabled = true;
        }
        else if (supSlot == null)
        {
            supSlot = item;
            supSlotImage.sprite = item.icon;
            supSlotImage.enabled = true;
        }
    }

    public void UseMain()
    {
        if (mainSlot == null || isCooldownActive) return;


        if (mainSlot.ability != null)
        {
            Debug.Log("Using ability: " + mainSlot.ability.name);
            mainSlot.ability.Activate(gameObject);

            StartCoroutine(CooldownCoroutine());
        }


        // Reset the mainSlot and its UI image after use
        mainSlot = null;
        mainSlotImage.sprite = null;
        mainSlotImage.enabled = false;

        // If there is an item in the supSlot, move it to mainSlot
        if (supSlot != null)
        {
            mainSlot = supSlot;
            mainSlotImage.sprite = mainSlot.icon;
            mainSlotImage.enabled = true;

            supSlot = null;
            supSlotImage.sprite = null;
            supSlotImage.enabled = false;
        }
    }

    private IEnumerator CooldownCoroutine()
    {
        isCooldownActive = true;

        yield return new WaitForSeconds(cooldownTime);

        isCooldownActive = false;
    }
}

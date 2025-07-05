using UnityEngine;
using UnityEngine.UI;

public class EquipmentManager : MonoBehaviour
{
    [Header("UI Slot Images")]
    public Image mainSlotImage;
    public Image supSlotImage;

    ItemSO mainSlot;
    ItemSO supSlot;

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
        if (mainSlot == null) return;
        mainSlot = null;
        mainSlotImage.sprite = null;
        mainSlotImage.enabled = false;

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
    public void UseSup()
    {
        if (supSlot == null) return;

        supSlot = null;
        supSlotImage.sprite = null;
        supSlotImage.enabled = false;
    }
}

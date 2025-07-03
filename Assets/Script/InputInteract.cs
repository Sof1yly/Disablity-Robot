using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputInteract : MonoBehaviour
{
    [SerializeField] List<Input_Interact> InputKey = new List<Input_Interact>();

    void Update()
    {
        foreach (var key in InputKey)
        {
            if (Input.GetKeyDown(key.InteractKey))
            {
                key.PressButton();
            }
        }
    }
}
[System.Serializable]
public class Input_Interact
{


    public KeyCode InteractKey => interactKey;
    [SerializeField] KeyCode interactKey;
    [SerializeField] UnityEvent buttonPress;

    public void PressButton()
    {
        buttonPress?.Invoke();
    }
}


using UnityEngine;
using UnityEngine.Events;

public abstract class ButtonAction : MonoBehaviour
{
    [SerializeField] protected KeyboardInput keyboard;
    [SerializeField] protected UnityEvent onSelect;
    [SerializeField] protected UnityEvent onPress;
    [SerializeField] protected UnityEvent onDeselect;

    public void OnSelect()
    {
        onSelect?.Invoke();
    }

    public void OnDeselect()
    {
        onDeselect?.Invoke();
    }

    public abstract void OnPress();
}

using UnityEngine;

public abstract class ButtonAction : MonoBehaviour
{
    [SerializeField] protected KeyboardInput keyboard;


    private void OnValidate()
    {
        keyboard = this.transform.parent.parent.GetComponent<KeyboardInput>();
    }

    public abstract void OnPress();
}

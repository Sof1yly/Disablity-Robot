using UnityEngine;

public class Locker : MonoBehaviour, IInteractable
{
    [SerializeField] private string _promt;
    public string InteractionPromt => _promt;
    public bool Interact(Interactor interactor)
    {
        Debug.Log("Unlock Locker!");
        return true;
    }
}

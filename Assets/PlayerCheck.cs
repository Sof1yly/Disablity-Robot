using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
public class PlayerCheck : MonoBehaviour
{
    [SerializeField] UnityEvent OnActive;
    [SerializeField] UnityEvent OffActive;

    public event Action<bool> StateUpdater;
    PlayerInput playerInput;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }


    public bool inputState { get; private set; }

    private void Start()
    {
        StateUpdater?.Invoke(inputState);
        OffActive?.Invoke();
    }

    private void Update()
    {
        if (playerInput.user != null && playerInput.actions["Restart"].ReadValue<float>() > 0)
        {
            UpdateState(true);
        }
    }

    [ContextMenu("Active Player Input")]
    void computeState()
    {
        inputState = true;
        OnActive?.Invoke();
        Debug.Log($"Player Join {playerInput.user}");
        StateUpdater?.Invoke(inputState);
    }

    void UpdateState(bool stateNewState)
    {
        if (stateNewState != inputState)
        {
            computeState();
        }
    }
}

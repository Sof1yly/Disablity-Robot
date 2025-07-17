using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerCheck : MonoBehaviour
{
    public event Action<bool> StateUpdater;

    PlayerInput playerInput;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    bool inputState;

    private void Start()
    {
        StateUpdater?.Invoke(inputState);
    }

    private void Update()
    {
        UpdateState(playerInput.user != null);
    }

    void UpdateState(bool stateNewState)
    {
        if (stateNewState != inputState)
        {
            Debug.Log("Player Join");
            inputState = stateNewState;
            StateUpdater?.Invoke(inputState);
        }
    }
}

using System;
using UnityEngine;
using UnityEngine.Events;

public class OnMinigameStateUpdate_Listener : MonoBehaviour
{

    [SerializeField] UnityEvent OnWin;
    [SerializeField] UnityEvent OnLoses;
    public event Action<bool> OnMingameStateUpdate;
    private void Awake()
    {
        FindAnyObjectByType<PhaseController>().MiniGameStateUpdateEvent += OnMiniGameWinStateUpdate;

    }
    public void OnMiniGameWinStateUpdate(bool isWin)
    {
        if (isWin)
        {
            OnWin?.Invoke();
        }
        else
        {
            OnLoses?.Invoke();
        }


        OnMingameStateUpdate?.Invoke(isWin);
    }
}

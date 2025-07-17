using System;
using UnityEngine;
using UnityEngine.Events;

public class PhaseListener : MonoBehaviour
{
    [SerializeField] Phase activePhase;


    public event Action OnActiveEvent;
    public event Action OnUnActiveEvent;

    [Header("Active/UnActive Event")]
    [SerializeField] UnityEvent OnActive;
    [SerializeField] UnityEvent OnUnActive;

    [SerializeField] bool enableSetActiveObj;
    private void Awake()
    {
        FindAnyObjectByType<PhaseController>().PhaseUpdateEvent += UpdatePhase;

    }




    void UpdatePhase(Phase phase)
    {
        bool isMyPhase = phase == activePhase;
        if (enableSetActiveObj)
        {
            this.gameObject.SetActive(isMyPhase);
        }

        if (isMyPhase)
        {
            OnActiveEvent?.Invoke();
            OnActive?.Invoke();
        }
        else
        {
            OnUnActiveEvent?.Invoke();
            OnUnActive?.Invoke();
        }



    }
}

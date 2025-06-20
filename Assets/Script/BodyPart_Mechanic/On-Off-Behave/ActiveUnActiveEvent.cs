using UnityEngine;
using UnityEngine.Events;

public class ActiveUnActiveEvent : Activeable
{
    [SerializeField] UnityEvent activeEvent;
    [SerializeField] UnityEvent unActiveEvent;
    protected override void Active()
    {
        activeEvent?.Invoke();
    }

    protected override void InActive()
    {
        unActiveEvent.Invoke();
    }
}

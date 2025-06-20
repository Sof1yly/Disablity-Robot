using System;
using UnityEngine;

public abstract class Activeable : MonoBehaviour
{

    public void SetUpEvent(ref Action OnActive, ref Action OnInactive)
    {
        OnActive += Active;
        OnInactive += InActive;
    }

    protected abstract void Active();
    protected abstract void InActive();

}

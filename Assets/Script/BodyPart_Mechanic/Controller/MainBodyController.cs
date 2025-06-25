using System;
using UnityEngine;
public class MainBodyController : MainModule<MainBodyController>
{
    public event Action<Body[]> OnBodyChanged;

    Body[] currentBody = new Body[3];

    public void SetNewBody(Body[] newBody)
    {
        currentBody = new Body[3];
        for (int i = 0; i < currentBody.Length; i++)
        {
            currentBody[i] = newBody[i];
            Debug.Log("Played");
        }

        OnBodyChanged?.Invoke(currentBody);
    }
}




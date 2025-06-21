using System;
public class MainBodyController : MainModule<MainBodyController>
{
    public event Action<Body[]> OnBodyChanged;

    Body[] currentBody = new Body[2];

    public void SetNewBody(Body[] newBody)
    {
        for (int i = 0; i < currentBody.Length; i++)
        {
            currentBody[i] = newBody[i];
        }
        OnBodyChanged?.Invoke(currentBody);
    }
}




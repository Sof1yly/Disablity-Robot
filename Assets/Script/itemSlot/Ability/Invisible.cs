using UnityEngine;

public class Invisible : MonoBehaviour
{
    public GameObject playerVisual;
    public void SetInvisible()
    {
        playerVisual.SetActive(false);
    }

    public void SetSeen()
    {
        playerVisual.SetActive(true);
    }
}



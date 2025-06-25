using UnityEngine;

public class SetActive : MonoBehaviour
{
    [SerializeField] GameObject scriptHolder;

    public void setActive(GameObject scriptHolder)
    {
        scriptHolder.SetActive(true);
    }
    public void setInActive(GameObject scriptHolder)
    {
        scriptHolder.SetActive(false);
    }
}

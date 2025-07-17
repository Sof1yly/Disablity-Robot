using UnityEngine;
using UnityEngine.Events;

public class WaitPlayerActiveInput : MonoBehaviour
{
    PlayerCheck[] playerCheck;
    void Start()
    {
        playerCheck = FindObjectsByType<PlayerCheck>(FindObjectsSortMode.None);
    }
    bool state = false;
    // Update is called once per frame

    [SerializeField] UnityEvent OnAllPlayerActiveInput;
    void Update()
    {
        if (Check() != state && state == false)
        {
            state = true;
            OnAllPlayerActiveInput?.Invoke();
        }
    }
    [ContextMenu("Skip Wait")]
    void Skip()
    {
        state = true;
        OnAllPlayerActiveInput?.Invoke();
    }


    bool Check()
    {
        foreach (PlayerCheck check in playerCheck)
        {
            if (check.inputState == false)
            {
                return false;
            }
        }
        return true;
    }
}

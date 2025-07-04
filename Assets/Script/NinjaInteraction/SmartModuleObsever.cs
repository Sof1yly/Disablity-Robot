using UnityEngine;

public class SmartModuleObsever : MonoBehaviour, IObserver
{
    private bool isSmartModuleOn = false;

    public void OnNotify(string eventType)
    {
        if (eventType == "SmartModule Activate")
        {
            isSmartModuleOn = !isSmartModuleOn;
            Debug.Log("SmartModule is now: " + (isSmartModuleOn ? "ON" : "OFF"));
        }
    }

    void Start()
    {
        Subject subject = FindFirstObjectByType<Subject>();
        if (subject != null)
        {
            subject.RegisterObserver(this);
        }
    }

    void OnDestroy()
    {
        Subject subject = FindFirstObjectByType<Subject>();
        if (subject != null)
        {
            subject.UnregisterObserver(this);
        }
    }
}

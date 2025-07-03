using UnityEngine;

public class TrackUpdate : MonoBehaviour
{
    public Tracker currentTracker { get; private set; }

    public void SetTracker(Tracker newTracker)
    {
        currentTracker = newTracker;
    }

    public void OnFinishOneRound()
    {
        Debug.Log("Yippe!!!!!!");
    }
}

using UnityEngine;


public class Tracker : MonoBehaviour
{
    [SerializeField] int trackerType;
    public int GetTrackerType => trackerType;
    [SerializeField] bool isLastTrack;
    public bool IsLastTrack => isLastTrack;
    [SerializeField] bool IsWinFlag;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<TrackUpdate>(out TrackUpdate trackerUpdate))
        {
            updateTracker(trackerUpdate);
        }
    }


    void updateTracker(TrackUpdate trackerUpdate)
    {
        if (trackerType == 1 && IsWinFlag == false)
        {
            trackerUpdate.SetTracker(this);
            return;
        }


        if (IsWinFlag && trackerUpdate.currentTracker.IsLastTrack)
        {
            trackerUpdate.SetTracker(this);
            trackerUpdate.OnFinishOneRound();
            return;
        }


        if (trackerUpdate.currentTracker.GetTrackerType + 1 == trackerType)
        {
            trackerUpdate.SetTracker(this);
        }

    }
}

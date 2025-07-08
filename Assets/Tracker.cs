using UnityEngine;


public class Tracker : MonoBehaviour
{
    [SerializeField] float trackerType;
    public float GetTrackerType => trackerType;
    [SerializeField] bool isLastTrack;
    public bool IsLastTrack => isLastTrack;
    [SerializeField] bool IsAutoTrack = true;
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
        if (IsAutoTrack && IsWinFlag == false)
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

        if (Mathf.Abs(trackerUpdate.currentTracker.GetTrackerType - trackerType) <= 1 && Mathf.Abs(trackerUpdate.currentTracker.GetTrackerType - trackerType) > 0)
        {
            trackerUpdate.SetTracker(this);
        }

    }
}

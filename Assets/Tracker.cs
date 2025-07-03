using UnityEngine;

public enum TrackerType
{
    WinTrack, EarlyTrack, MiddleTrack, LastTrack
}

public class Tracker : MonoBehaviour
{
    [SerializeField] TrackerType trackerType;
    public TrackerType GetTrackerType => trackerType;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<TrackUpdate>(out TrackUpdate trackerUpdate))
        {
            updateTracker(trackerUpdate);
        }
    }


    void updateTracker(TrackUpdate trackerUpdate)
    {
        if (trackerType == TrackerType.EarlyTrack)
        {
            trackerUpdate.SetTracker(this);
            return;
        }

        if (trackerUpdate.currentTracker == null) return;

        if (trackerType == TrackerType.MiddleTrack && trackerUpdate.currentTracker.trackerType == TrackerType.EarlyTrack)
        {
            trackerUpdate.SetTracker(this);
            return;
        }

        if (trackerType == TrackerType.LastTrack && trackerUpdate.currentTracker.trackerType == TrackerType.MiddleTrack)
        {
            trackerUpdate.SetTracker(this);
            return;
        }

        if (trackerType == TrackerType.WinTrack && trackerUpdate.currentTracker.trackerType == TrackerType.LastTrack)
        {
            trackerUpdate.OnFinishOneRound();
            trackerUpdate.SetTracker(null);
            return;
        }
    }
}

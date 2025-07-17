using UnityEngine;


public class Tracker : MonoBehaviour
{
    [SerializeField] float trackerindex;
    public float GetTrackerindex => trackerindex;
    [SerializeField] bool isLastTrack;
    public bool IsLastTrack => isLastTrack;
    [SerializeField] bool IsAutoTrack = true;
    [SerializeField] bool IsWinFlag;

    public Transform futureTrack { get; private set; }
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

        if (trackerUpdate.currentTracker.GetTrackerindex - trackerindex <= 1 && trackerUpdate.currentTracker.GetTrackerindex - trackerindex > 0)
        {
            trackerUpdate.SetTracker(this);
        }

    }
}

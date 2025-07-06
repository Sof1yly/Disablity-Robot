using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerRankUpdate : MonoBehaviour
{
    [SerializeField] List<TrackUpdate> trackerList = new List<TrackUpdate>();
    private void Update()
    {
        updateRanking();
    }

    void updateRanking()
    {
        trackerList = trackerList
            .OrderBy(i => i.FinalRank)
            .ThenByDescending(i => i.CurrentRoundPlay)
            .ThenByDescending(i => i.currentTracker.GetTrackerType)
            .ThenByDescending(i => Vector3.Distance(i.transform.position, i.currentTracker.transform.position))
            .ToList();

        for (int i = 0; i < trackerList.Count; i++)
        {
            trackerList[i].UpdateRanking(i + 1);
        }

    }
}
[System.Serializable]
public struct computedTrackUpdate
{
    public TrackUpdate tracker { get; private set; }
    public computedTrackUpdate(TrackUpdate setTrack)
    {
        tracker = setTrack;
    }
}

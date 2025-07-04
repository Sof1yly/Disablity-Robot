using UnityEngine;
using UnityEngine.Events;

public class TrackUpdate : MonoBehaviour
{
    [SerializeField] Tracker startTrack;
    public Tracker currentTracker { get; private set; }
    int currentRoundPlay = 0;
    public int CurrentRoundPlay => currentRoundPlay;
    int MaxRound;
    int finalRank = 5; public int FinalRank => finalRank;

    IsGameEnd gameEndController;
    [SerializeField] UnityEvent<int> OnRankingUpdate;

    private void Start()
    {
        SetTracker(startTrack);
        gameEndController = FindAnyObjectByType<IsGameEnd>();
        MaxRound = gameEndController.RoundToFinish;
    }

    public void SetTracker(Tracker newTracker)
    {
        currentTracker = newTracker;
    }

    public void OnFinishOneRound()
    {
        currentRoundPlay++;
        if (currentRoundPlay >= MaxRound)
        {
            gameEndController.OnPlayerFinishAllRound(this);
        }
    }

    public void UpdateFinalRanking(int FinalRank)
    {
        finalRank = FinalRank;
    }

    public void UpdateRanking(int Rank)
    {

        OnRankingUpdate?.Invoke(Rank);
    }
}

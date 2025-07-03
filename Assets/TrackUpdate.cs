using UnityEngine;

public class TrackUpdate : MonoBehaviour
{
    public Tracker currentTracker { get; private set; }
    int currentRoundPlay = 0;
    int MaxRound;
    IsGameEnd gameEndController;
    private void Start()
    {
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
            gameEndController.OnPlayerFinishAllRound(this.gameObject);
        }
    }
}

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class TrackUpdate : MonoBehaviour
{
    [SerializeField] Tracker startTrack;
    public Tracker currentTracker { get; private set; }
    int currentRoundPlay = 0;
    public int CurrentRoundPlay => currentRoundPlay;
    int MaxRound;
    int finalRank = 5;
    public int FinalRank => finalRank;
    public int CurrentRank { get; private set; }
    

    IsGameEnd gameEndController;
    [SerializeField] UnityEvent<int> OnRankingUpdate;
    [SerializeField] UnityEvent<int> OnLapUpdate;
    [SerializeField] float RestartCD = 1;

    Rigidbody rb;

    float SnapTime;
    private void Start()
    {
        playerInput = this.transform.parent.GetComponentInChildren<PlayerInput>();
        rb = GetComponent<Rigidbody>();
        SetTracker(startTrack);
        gameEndController = FindAnyObjectByType<IsGameEnd>();
        MaxRound = gameEndController.RoundToFinish;
    }

    public void RestartCar()
    {
        if (Time.time < SnapTime) return;
        rb.linearVelocity = Vector3.zero;
        SnapTime = Time.time + RestartCD;
        this.transform.position = currentTracker.transform.position;
        this.transform.rotation = currentTracker.transform.rotation;
    }

    public void SetTracker(Tracker newTracker)
    {
        currentTracker = newTracker;
    }
    public void OnFinishOneRound()
    {
        currentRoundPlay++;
        OnLapUpdate?.Invoke(currentRoundPlay);
        if (currentRoundPlay >= MaxRound)
        {
            gameEndController.OnPlayerFinishAllRound(this);
        }
    }
    public void UpdateFinalRanking(int FinalRank)
    {
        finalRank = FinalRank;
    }

    PlayerInput playerInput;
    public void UpdateRanking(int Rank)
    {
        CurrentRank = Rank;
        Debug.Log($"Player  {playerInput.playerIndex} , Current Rank {Rank}");
        this.transform.parent.gameObject.name = $"Player : {playerInput.playerIndex} Rank : {Rank}";
        OnRankingUpdate?.Invoke(Rank);
    }
    
}

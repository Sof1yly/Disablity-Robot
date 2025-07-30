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
    [SerializeField] UnityEvent<int> OnPlayerFinishRound;
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
    [SerializeField] AudioClip Lap1;
    [SerializeField] AudioClip Lap2;
    [SerializeField] AudioClip Lap3;
    [SerializeField] AudioClip PlayerFinish;
    public void OnFinishOneRound()
    {
        currentRoundPlay++;
        OnLapUpdate?.Invoke(currentRoundPlay);
        if(currentRoundPlay == 1)
        {
            SoundPlayer.Instance.PlaySound(Lap1, 0);
        }
        else if (currentRoundPlay == 2)
        {
            SoundPlayer.Instance.PlaySound(Lap2, 0);
        }
        else if (currentRoundPlay == 3)
        {
            SoundPlayer.Instance.PlaySound(Lap3, 0);
        }

        if (currentRoundPlay >= MaxRound)
        {
            SoundPlayer.Instance.PlaySound(PlayerFinish, 0);
            OnFinishEveryRound();
        }
    }
    public void UpdateFinalRanking(int FinalRank)
    {
        finalRank = FinalRank;
    }
    public void OnFinishEveryRound()
    {
        gameEndController.OnPlayerFinishAllRound(this);
        OnPlayerFinishRound?.Invoke(FinalRank);
          
    }

    PlayerInput playerInput;
    public void UpdateRanking(int Rank)
    {
        CurrentRank = Rank;
        Debug.Log($"Player  {playerInput.playerIndex} , Current Rank {Rank}");
        this.transform.parent.gameObject.name = $"Player : {playerInput.playerIndex + 1} Rank : {Rank}";
        OnRankingUpdate?.Invoke(Rank);
    }
    
}

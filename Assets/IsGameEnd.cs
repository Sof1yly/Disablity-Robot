using MoreMountains.Feedbacks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IsGameEnd : MonoBehaviour
{
    List<TrackUpdate> finishedList = new List<TrackUpdate>();
    [SerializeField] int roundToFinish = 0;
    public int RoundToFinish => roundToFinish;
    [SerializeField] int debugMaxPlayer = 1;

    [SerializeField] UnityEvent OnGameFinish;

    public void OnPlayerFinishAllRound(TrackUpdate Player)
    {
        if (finishedList.Contains(Player)) return;
        finishedList.Add(Player);
        int ranking = finishedList.IndexOf(Player);
        Player.UpdateFinalRanking(ranking + 1);
        isGameEnd();
        //Debug.Log("Yippe! , Player Finish da Game NOWWWWWW");
    }
    [SerializeField] AudioClip allFinish;
    [SerializeField] MMF_Player mmfPlayer;
    public void isGameEnd()
    {
        if (finishedList.Count == debugMaxPlayer)
        {
            SoundPlayer.Instance.PlaySound(allFinish, 0);
            mmfPlayer.PlayFeedbacks();
            Debug.Log("Game Ended");
            OnGameFinish?.Invoke();
        }
    }
}

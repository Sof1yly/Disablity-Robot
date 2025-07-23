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

    public void isGameEnd()
    {
        if (finishedList.Count == debugMaxPlayer)
        {
            Debug.Log("Game Ended");
            OnGameFinish?.Invoke();
        }
    }
}

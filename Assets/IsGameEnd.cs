using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IsGameEnd : MonoBehaviour
{
    List<GameObject> finishedList = new List<GameObject>();
    [SerializeField] int roundToFinish = 0;
    public int RoundToFinish => roundToFinish;
    [SerializeField] int debugMaxPlayer = 1;

    [SerializeField] UnityEvent OnGameFinish;

    public void OnPlayerFinishAllRound(GameObject Player)
    {
        finishedList.Add(Player);
        Debug.Log("Yippe! , Player Finish da Game NOWWWWWW");
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

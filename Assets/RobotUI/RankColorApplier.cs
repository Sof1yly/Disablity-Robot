using UnityEngine;
using UnityEngine.UI;

public class RankColorApplier : MonoBehaviour
{
    [Header("References")]
    public Image targetImage;
    public RankColorStorage rankColorStorage;

    [Header("Current Rank")]
    [Tooltip("Set this to change the player's rank")]
    public int playerRank;

    void Start()
    {
        ApplyRankColor(playerRank);
    }

    public void SetRank(int newRank)
    {
        playerRank = newRank;
        ApplyRankColor(playerRank);
    }

    private void ApplyRankColor(int rank)
    {
        if (targetImage != null && rankColorStorage != null)
        {
            Color rankColor = rankColorStorage.GetColorByRank(rank);
            targetImage.color = rankColor;
        }
        else
        {
            Debug.LogWarning("Image or RankColorStorage not assigned.");
        }
    }
}

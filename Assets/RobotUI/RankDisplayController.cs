using UnityEngine;

public class RankDisplayController : MonoBehaviour
{
    public RankColorApplier colorApplier;
    public UpdateRank rankTextUpdater;

    public void SetPlayerRank(int rank)
    {
        if (colorApplier != null)
            colorApplier.SetRank(rank);

        if (rankTextUpdater != null)
            rankTextUpdater.UpdateRankText(rank);
    }
}

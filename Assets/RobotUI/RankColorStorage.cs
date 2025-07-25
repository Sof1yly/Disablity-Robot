using UnityEngine;

[System.Serializable]
public class RankColor
{
    public int rank;
    public Color color;
}

[CreateAssetMenu(fileName = "RankColorStorage", menuName = "GameData/RankColorStorage")]
public class RankColorStorage : ScriptableObject
{
    public RankColor[] rankColors;

    public Color GetColorByRank(int rank)
    {
        foreach (var rc in rankColors)
        {
            if (rc.rank == rank)
                return rc.color;
        }

        return Color.white; // Default color if rank not found
    }
}

using TMPro;
using UnityEngine;

public class UpdateRank : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro;

    private void Awake()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateRankText(int rank)
    {
        if (textMeshPro == null)
            textMeshPro = GetComponent<TextMeshProUGUI>();

        textMeshPro.text = rank.ToString() + GetRankSuffix(rank);
    }

    private string GetRankSuffix(int rank)
    {
        // Ensure rank is between 1 and 4
        if (rank == 1)
            return "st";
        if (rank == 2)
            return "nd";
        if (rank == 3)
            return "rd";
        if (rank == 4)
            return "th";

        return ""; // Fallback, but shouldn't be needed with only 4 players
    }
}

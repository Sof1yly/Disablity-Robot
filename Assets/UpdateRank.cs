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

        textMeshPro.text = rank.ToString();
    }
}

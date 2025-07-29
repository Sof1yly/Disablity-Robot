using TMPro;
using UnityEngine;
public class UpdateRank : MonoBehaviour
{
    TextMeshProUGUI textMeshProgui;

    private void Start()
    {
        textMeshProgui = GetComponent<TextMeshProUGUI>();
    }

    public void OnEnable()
    {
        textMeshProgui = GetComponent<TextMeshProUGUI>();
    }
    public void UpdateRankText(int Rank)
    {
        textMeshProgui.text = Rank.ToString();
    }
}

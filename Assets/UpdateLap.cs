using TMPro;
using UnityEngine;

public class UpdateLap : MonoBehaviour
{
    TextMeshProUGUI textMeshProgui;

    private void Start()
    {
        textMeshProgui = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateLapText(int Lap)
    {
        Lap.ToString();
        textMeshProgui.text = ($"{Lap} / 3");
    }
}

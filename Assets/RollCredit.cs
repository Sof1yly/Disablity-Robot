using TMPro;
using UnityEngine;

public class RollCredit : MonoBehaviour
{
    [SerializeField] TMP_Text LeftCredit;
    [SerializeField] TMP_Text CenterCredit;
    [SerializeField] TMP_Text RightCredit;
    [SerializeField] int speed = 3;

    void Update()
    {
        LeftCredit.rectTransform.anchoredPosition += Vector2.up * Time.deltaTime * speed;
        CenterCredit.rectTransform.anchoredPosition += Vector2.up * Time.deltaTime * speed;
        RightCredit.rectTransform.anchoredPosition += Vector2.up * Time.deltaTime * speed;
    }
}

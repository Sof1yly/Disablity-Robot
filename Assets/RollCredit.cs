using TMPro;
using UnityEngine;

public class RollCredit : MonoBehaviour
{
    [SerializeField] TMP_Text credit;
    [SerializeField] int speed = 3;

    void Update()
    {
        credit.rectTransform.anchoredPosition += Vector2.up * Time.deltaTime * speed;
    }
}

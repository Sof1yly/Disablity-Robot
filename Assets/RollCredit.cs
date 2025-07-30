using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RollCredit : MonoBehaviour
{
    [SerializeField] List<GameObject> Credits = new List<GameObject>();
    [SerializeField] int speed = 3;

    void Update()
    {
        foreach (var r in Credits)
        {
            RectTransform RT = r.GetComponent<RectTransform>();
            RT.anchoredPosition += Vector2.up * Time.deltaTime * speed;
        }
    }
}

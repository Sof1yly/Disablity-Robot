using UnityEngine;

[CreateAssetMenu(fileName = "BodySO", menuName = "Body")]
public class Body : ScriptableObject
{
    [SerializeField] private GameObject partPrefabs;
}

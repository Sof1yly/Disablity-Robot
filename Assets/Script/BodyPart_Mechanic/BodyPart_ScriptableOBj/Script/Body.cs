using UnityEngine;

[CreateAssetMenu(fileName = "BodySO", menuName = "Body")]
public class Body : ScriptableObject
{
    [SerializeField] private GameObject partPrefabs;
    [SerializeField] private string partName;

    public GameObject PartPrefabs
    {
        get { return partPrefabs; }
        set { partPrefabs = value; }
    }
    public string PartName
    {
        get { return partName; }
        set { partName = value; }
    }
}

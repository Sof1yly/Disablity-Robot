using UnityEngine;

[CreateAssetMenu(fileName = "CoreSwapSO", menuName = "CoreSwap/CoreSwapSO")]
public class CoreSwapSO : ScriptableObject
{
    [Header("Raycast settings")]
    public float maxDistance = 10f;
    public LayerMask coreLayerMask; //coreLayer

    [Header("Prefab Setting")]
    public GameObject corePrefab; //body_core the thing you wanna spawn

    [Header("Input Settings")]
    public KeyCode swapKey = KeyCode.T;

    [Header("Swap Behavior")]
    public float clickDelay = 0.5f;
    public float Offset = 1.5f;
}

using UnityEngine;

public class TempBodySwap : MonoBehaviour
{
    [SerializeField] int BodyIndex;
    [SerializeField] Body SwapBody;
    [SerializeField] PartSpawner partSpawner;
    TempBodySwapperManage TempBodySwapperManage;

    private void Start()
    {
        TempBodySwapperManage = FindAnyObjectByType<TempBodySwapperManage>();
        
    }

    [ContextMenu("Add NewBody")]
    public void SwapingBody()
    {
        TempBodySwapperManage.AddNewBody(BodyIndex, SwapBody);
    }

    [ContextMenu("Remove Body")]
    public void RemoveBody()
    {
        TempBodySwapperManage.RemoveBody(BodyIndex);
        
        partSpawner.SpawnPart(SwapBody); 

    }



}

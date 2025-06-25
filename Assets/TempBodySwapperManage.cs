using System.Linq;
using UnityEngine;

public class TempBodySwapperManage : MonoBehaviour
{
    [SerializeField] MainBodyController mainBodyController;
    [SerializeField] StateChanger stateChanger;
    Body[] storedBody = new Body[3];

    private void Start()
    {
        Body[] storedBody = new Body[3];
        UpdateState();
    }

    public void AddNewBody(int index, Body newBody) // not 0 but + 1 
    {
        storedBody[index - 1] = newBody;
        UpdateState();
    }

    public void RemoveBody(int index)
    {
        storedBody[index - 1] = null;
        UpdateState();
    }

    void UpdateState()
    {
        stateChanger.AssignNewBody(storedBody.ToList());
    }



}

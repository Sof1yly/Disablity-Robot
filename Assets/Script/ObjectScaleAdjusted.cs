using UnityEngine;

public class ObjectScaleAdjusted : MonoBehaviour
{
    [SerializeField]
    GameObject changedObject;

    public void SetScaleY(float ScaleY)
    {
        changedObject.transform.localScale = new Vector3(changedObject.transform.localScale.x, ScaleY, changedObject.transform.localScale.z);
    }
}

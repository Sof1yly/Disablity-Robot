using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Ability/CallTeacher")]
public class CallTeach : ItemAbility
{
    public GameObject teacherPrefab;

    public int teacherCount = 1;

    public Vector3 spawnOffset = new Vector3(2, 6, 0);

    public override void Activate(GameObject user)
    {
        Vector3 origin = user.transform.position + spawnOffset;
        Quaternion rot = user.transform.rotation;
        for (int i = 0; i < teacherCount; i++)
        {
            Instantiate(teacherPrefab, origin, rot);
        }
    }
}

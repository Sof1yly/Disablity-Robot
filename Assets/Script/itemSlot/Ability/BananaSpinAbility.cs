using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Ability/ItemSO")]

public class BananaSpinAbility : ItemAbility
{
    public float spinDuration = 1f;
    public float spinSpeed = 360f;

    public override void Activate(GameObject target)
    {
        var mb = target.GetComponent<MonoBehaviour>();

        if (mb != null)
        {
            mb.StartCoroutine(SpinCoroutine(target.transform));
        }
    }

    IEnumerator SpinCoroutine(Transform t)
    {
        float elapsed = 0f;
        while(elapsed < spinDuration)
        {
            t.Rotate(0f, spinSpeed * Time.deltaTime, 0f, Space.Self);
            elapsed += Time.deltaTime;
            yield return null; 
        }
    }

}

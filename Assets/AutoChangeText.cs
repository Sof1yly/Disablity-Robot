using TMPro;
using UnityEngine;

public class AutoChangeText : MonoBehaviour
{
    [SerializeField] bool AutoChange = true;

    private void OnValidate()
    {
        this.GetComponent<TextMeshProUGUI>().text = this.transform.parent.name;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

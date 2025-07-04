using UnityEngine;

public class MainModule<T> : MonoBehaviour where T : MonoBehaviour
{

    protected void Awake()
    {
        T component = GetComponent<T>();
        if (component != null)
        {
            ObjectFinder.Instance.AddComponent(component);
        }

        StartUp();
    }

    protected virtual void StartUp()
    {

    }


    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {

    }

}

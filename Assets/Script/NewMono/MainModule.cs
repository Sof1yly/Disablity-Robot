using UnityEngine;

public class MainModule : MonoBehaviour
{

    protected void Awake()
    {
        ObjectFinder.Instance.AddComponent(this);
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

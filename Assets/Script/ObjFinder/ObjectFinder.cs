using System;
using System.Collections.Generic;
using UnityEngine;
public class ObjectFinder : Singleton<ObjectFinder>
{
    private Dictionary<Type, MonoBehaviour> componentMap = new Dictionary<Type, MonoBehaviour>();

    public void AddComponent<T>(T component) where T : MonoBehaviour
    {
        componentMap[typeof(T)] = component;
    }

    public T GetStoredComponent<T>() where T : MonoBehaviour
    {
        if (componentMap.TryGetValue(typeof(T), out var comp))
            return comp as T;
        return null;
    }
}

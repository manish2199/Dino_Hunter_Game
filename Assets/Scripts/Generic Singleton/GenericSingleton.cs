using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericSingleton<T> : MonoBehaviour where T : GenericSingleton<T>
{
    private static T instance;

    public static T Instance { get{ return instance; } protected set { instance = value; } }

    protected virtual void Awake()
    {
        if( instance == null)
        {
            instance = this as T;
            // DontDestroyOnLoad(this as T);
        }
        // else
        // {
            // Destroy(this as T);
        // }
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericSingleton<T> : MonoBehaviour where T : GenericSingleton<T> 
{
    private static T instance ;

    public static T Instance { get; }

    void Awake()
    {
        if(instance != null)
        {
           Destroy(gameObject);
        }
        else
        {
            instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
    }

}

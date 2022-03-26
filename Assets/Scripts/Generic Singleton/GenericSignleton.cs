using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericSignleton<T> : MonoBehaviour where T : GenericSignleton<T> 
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

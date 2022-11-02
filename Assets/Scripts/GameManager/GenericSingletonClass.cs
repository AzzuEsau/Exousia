using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericSingletonClass<GenericComponent> : MonoBehaviour where GenericComponent : Component
{
    private static GenericComponent _instance;

    public virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as GenericComponent;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
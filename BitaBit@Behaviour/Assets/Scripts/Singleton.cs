using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : DontDestroyOnLoad where T : Singleton<T>
{
    protected static T m_Instance;

    public static T Instance
    {
        get { return m_Instance; }
    }

    protected override void Awake()
    {
        if (m_Instance == null)
        {
            m_Instance = (T)this;
        }
        else
        {
            Destroy(gameObject);
        }
        base.Awake();
    }
}

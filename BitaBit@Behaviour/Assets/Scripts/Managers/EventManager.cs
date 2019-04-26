using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }

    private Dictionary<EEventID, Action<object>> m_EventDict;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(this);
        m_EventDict = new Dictionary<EEventID, Action<object>>();
    }

    public void RegisterEvent(EEventID a_Key, Action<object> a_Action)
    {
        if (m_EventDict.ContainsKey(a_Key))
        {
            m_EventDict[a_Key] += a_Action;
        }
        else
        {
            m_EventDict.Add(a_Key, a_Action);
        }
    }

    public void UnregisterEvent(EEventID a_Key, Action<object> a_Action)
    {
        if (m_EventDict.ContainsKey(a_Key))
        {
            m_EventDict[a_Key] -= a_Action;
        }
        else
        {
            Debug.LogError("Event doesn't exist");
        }
    }

    public void DispatchEvent(EEventID a_Key, object a_Param = null)
    {
        if (m_EventDict.ContainsKey(a_Key))
        {
            m_EventDict[a_Key](a_Param);
        }
        else
        {
            Debug.LogError("Dispachted event doesn't exist");
            Debug.Log(a_Key);
        }
    }
}

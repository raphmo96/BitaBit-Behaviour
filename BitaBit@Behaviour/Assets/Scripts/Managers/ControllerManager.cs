using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class ControllerManager : Singleton<ControllerManager>
{

    public bool IsReady { get; set; }

    private Dictionary<SpiderController.EPlayer, InputDevice> m_PlayerDevices = new Dictionary<SpiderController.EPlayer, InputDevice>();

    public void SetPlayerDevice(SpiderController.EPlayer a_PlayerID, InputDevice a_Device)
    {
        if(!m_PlayerDevices.ContainsKey(a_PlayerID))
        {
            m_PlayerDevices.Add( a_PlayerID, a_Device);
        }
    }

    public InputDevice GetPlayerDevice(SpiderController.EPlayer a_PlayerID)
    {
        if (m_PlayerDevices.ContainsKey(a_PlayerID))
        {
            return m_PlayerDevices[a_PlayerID];
        }

        return null;
    }

    public bool ContainDevice(InputDevice a_Device)
    {
        return m_PlayerDevices.ContainsValue(a_Device);
    }

    public void ResetPlayerDevice()
    {
        m_PlayerDevices.Clear();
    }
}

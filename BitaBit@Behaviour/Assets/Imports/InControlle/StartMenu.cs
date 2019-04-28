using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using InControl;

public class StartMenu : MonoBehaviour 
{
    private int m_ActivatedDevices = 0;

	private void Start()
	{
        ControllerManager.Instance.ResetPlayerDevice();
	}

	void Update () 
	{
        InputDevice controller = InputManager.ActiveDevice;

        if (controller.GetControl(InputControlType.Action1).WasPressed && !ControllerManager.Instance.ContainDevice(controller))
        {
            ControllerManager.Instance.SetPlayerDevice((SpiderController.EPlayer)m_ActivatedDevices,controller);
			Debug.Log("REMOTE ADDED");
            m_ActivatedDevices++;
        }
        else
        {
        }

        if (m_ActivatedDevices >= 4)
        {
	        ControllerManager.Instance.IsReady = true;
	        Destroy(gameObject);
        }
    }
}
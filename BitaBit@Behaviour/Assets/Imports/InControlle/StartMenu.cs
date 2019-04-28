using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using InControl;

public class StartMenu : MonoBehaviour 
{
    private int m_ActivatedDevices = 0;

    [SerializeField]
    private List<GameObject> m_PressX;
    [SerializeField]
    private List<GameObject> m_ControllerIcon;

    [SerializeField]
    private TextMeshProUGUI m_TextTimer;

	private void Start()
	{
        m_TextTimer.gameObject.SetActive(false);
        for(int i = 0; i < m_ControllerIcon.Count; i++)
        {
            m_ControllerIcon[i].SetActive(false);
        }
        ControllerManager.Instance.ResetPlayerDevice();
	}

	private void Update () 
	{
        InputDevice controller = InputManager.ActiveDevice;

        if (controller.GetControl(InputControlType.Action1).WasPressed && !ControllerManager.Instance.ContainDevice(controller))
        {
            ControllerManager.Instance.SetPlayerDevice((SpiderController.EPlayer)m_ActivatedDevices,controller);
			Debug.Log("REMOTE ADDED");
            m_PressX[m_ActivatedDevices].SetActive(false);
            m_ControllerIcon[m_ActivatedDevices].SetActive(true);
            m_ActivatedDevices++;
            if(m_ActivatedDevices == 4)
            {
                StartCoroutine(StartGame());
            }
        }
        else
        {
        }

        if (m_ActivatedDevices >= 4)
        {
	        ControllerManager.Instance.IsReady = true;
        }
    }

    private IEnumerator StartGame()
    {
        m_TextTimer.gameObject.SetActive(true);
        for (int i = 3; i > 0; i--)
        {
            m_TextTimer.text = i + "...";
            yield return new WaitForSeconds(1f);
        }
        SceneLoadingManager.Instance.ChangeScene(EScenes.Timeline);
        yield return null;
    }
}
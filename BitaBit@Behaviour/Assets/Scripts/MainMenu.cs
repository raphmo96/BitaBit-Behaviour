using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject m_ChooseMenu;
    [SerializeField]
    private GameObject m_Button;
    private void Start()
    {
        m_ChooseMenu.SetActive(false);
        AudioManager.Instance.PlayMusic("MainMenu");
    }

    public void StartGame()
    {
        m_Button.SetActive(false);
        m_ChooseMenu.SetActive(true);
    }
}

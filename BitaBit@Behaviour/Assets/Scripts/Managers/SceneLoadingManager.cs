using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class SceneLoadingManager : Singleton<SceneLoadingManager>
{
    [SerializeField] private float m_LoadingFadeTime = 1f;
    [SerializeField] private TextMeshProUGUI m_LoadingText;
    [SerializeField] private Image m_LoadingScreen;
    [SerializeField] private Image m_LoadingIcon;

    private bool m_IsLoaded;
    private UnityEvent m_OnChangeScene;

    public UnityEvent OnChangeScene => m_OnChangeScene;

    protected override void Awake()
    {
        base.Awake();
    }

    private void OnLoadingDone(Scene a_Scene, LoadSceneMode a_LoadMode)
    {
        SceneManager.sceneLoaded -= OnLoadingDone;
        m_IsLoaded = true;
    }

    public void ChangeScene(EScenes a_SceneID, string a_LoadingMessage = "", bool a_IsSingle = true)
    {
        m_IsLoaded = false;
        
        SceneManager.sceneLoaded += OnLoadingDone;
        LoadSceneMode mode = a_IsSingle ? LoadSceneMode.Single : LoadSceneMode.Additive;
        
        if (a_IsSingle)
        {
            StartCoroutine(Loading(a_SceneID, mode));
        }
        else
        {
            SceneManager.LoadScene((int)a_SceneID, mode);
        }
    }

    private IEnumerator Loading(EScenes a_Name, LoadSceneMode a_Mode)
    {
        float m_CurrentTime = 0f;
        Color tempColor = Color.clear;

        m_LoadingScreen.gameObject.SetActive(true);
        m_LoadingText.gameObject.SetActive(true);

        while (m_CurrentTime < m_LoadingFadeTime)
        {
            float progress = (m_CurrentTime / m_LoadingFadeTime) + 0.1f;
            tempColor = Color.Lerp(Color.clear, Color.white, progress);

            m_LoadingText.color = tempColor;
            m_LoadingScreen.color = tempColor;
            m_LoadingIcon.color = tempColor;

            m_CurrentTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        m_CurrentTime = 0f;

        SceneManager.LoadScene((int)a_Name, a_Mode);

        while (!m_IsLoaded)
        {
            yield return new WaitForSeconds(0.5f);
        }

        while (m_CurrentTime < m_LoadingFadeTime)
        {
            tempColor = Color.Lerp(Color.white, Color.clear, m_CurrentTime / m_LoadingFadeTime);

            m_LoadingText.color = tempColor;
            m_LoadingScreen.color = tempColor;
            m_LoadingIcon.color = tempColor;

            m_CurrentTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        m_LoadingScreen.gameObject.SetActive(false);
        m_LoadingText.gameObject.SetActive(false);
    }
}

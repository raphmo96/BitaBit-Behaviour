using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitToChangeScene : MonoBehaviour
{
    public float m_TimeToWait = 15f;
    public EScenes m_SceneToChange = EScenes.MainMenu;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitAndChangeScene());
    }


    private IEnumerator WaitAndChangeScene()
    {
        yield return new WaitForSeconds(m_TimeToWait);
        SceneLoadingManager.Instance.ChangeScene(m_SceneToChange);
    }
}

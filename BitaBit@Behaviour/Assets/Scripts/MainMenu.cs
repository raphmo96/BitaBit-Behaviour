using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        AudioManager.Instance.PlayMusic("MainMenu");
    }

    public void StartGame()
    {
        SceneLoadingManager.Instance.ChangeScene(EScenes.Game);
    }
}

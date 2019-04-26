using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationLauncher : MonoBehaviour
{
    private void Start()
    {
        SceneLoadingManager.Instance.ChangeScene(EScenes.MainMenu);
    }
}

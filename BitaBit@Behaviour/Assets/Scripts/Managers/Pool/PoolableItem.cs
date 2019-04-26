using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolableItem : MonoBehaviour
{
    [SerializeField]
    private EPoolType m_PoolType;

    private void OnEnable()
    {
        SceneLoadingManager.Instance.OnChangeScene.AddListener(ReturnToPool);
    }

    private void OnDisable()
    {
        SceneLoadingManager.Instance.OnChangeScene.RemoveListener(ReturnToPool);
    }

    public void StartTimer(float a_Time)
    {
        StartCoroutine(WaitAndReturn(a_Time));
    }

    private IEnumerator WaitAndReturn(float a_Time)
    {
        yield return new WaitForSeconds(a_Time);
        ReturnToPool();
    }

    public void ReturnToPool()
    {
        StopAllCoroutines();
        PoolManager.Instance.ReturnToPool(m_PoolType, gameObject);
    }
}

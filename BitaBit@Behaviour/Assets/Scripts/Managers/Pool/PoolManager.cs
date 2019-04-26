using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PoolManager : Singleton<PoolManager>
{
    [SerializeField] private PoolTableDictionnary m_PoolTable;

    private Vector3 m_Pool_Limbo = new Vector3(-100f, -100f, -100f);
    private Dictionary<EPoolType, List<GameObject>> m_Pool = new Dictionary<EPoolType, List<GameObject>>();

    private void Start()
    {
        //Init pool item here
    }

    public GameObject GetFromPool(EPoolType a_Type, Vector3 a_Pos)
    {
        if (m_Pool.ContainsKey(a_Type))
        {
            if (m_Pool[a_Type].Count > 0)
            {
                GameObject go = m_Pool[a_Type][0];
                m_Pool[a_Type].Remove(go);
                go.transform.position = a_Pos;
                go.SetActive(true);
                return go;
            }
            else
            {
                GameObject go = Instantiate(m_PoolTable[a_Type]);
                go.transform.position = a_Pos;
                return go;
                //Instantiate and return;
            }

        }
        
        Debug.LogError("There is no: " + a_Type.ToString() + " in pool");
        return null;
    }

    public void ReturnToPool(EPoolType a_Type, GameObject a_Object)
    {
        if (m_Pool.ContainsKey(a_Type))
        {
            m_Pool[a_Type].Add(a_Object);
        }
        else
        {
            m_Pool.Add(a_Type, new List<GameObject>() { a_Object });
        }

        a_Object.SetActive(false);
        a_Object.transform.position = m_Pool_Limbo;
        a_Object.transform.SetParent(transform);
    }

    public void CreatePool(EPoolType a_Type, int number)
    {
        for (int i = 0; i < number; i++)
        {
            GameObject go = Instantiate(m_PoolTable[a_Type]);
            ReturnToPool(a_Type, go);
        }
    }
}

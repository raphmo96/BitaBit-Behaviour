using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class OutpostShop : MonoBehaviour
{
    private SphereCollider m_Collider;

    [SerializeField]
    private int m_OutPostIndex;

    private float m_ShopRadius = 40.0f;

    private void Start()
    {
        m_Collider = GetComponent<SphereCollider>();
        m_Collider.radius = m_ShopRadius;
    }

    
    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        UpgradeManager.Instance.SpendRessources();

        switch(m_OutPostIndex)
        {
            case 0: /* ADD ANTENNA */ break; 
            case 1: UpgradeManager.Instance.UpgradeLifeBar(); break;
            case 2: UpgradeManager.Instance.UpgradeRessourcesBar(); break;
            case 3: UpgradeManager.Instance.UpgradeLifeBar(); break;
            case 4: UpgradeManager.Instance.UpgradeRessourcesBar(); break;

        }

        // Make Asset appear *******
    }
}

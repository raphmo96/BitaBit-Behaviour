using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class OutpostShop : MonoBehaviour
{
    private SphereCollider m_Collider;

    [SerializeField]
    private int m_OutPostIndex;

    private float m_ShopRadius = 8.0f;

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
        PlayerManager.Instance.SpendRessources();
        GetComponentInChildren<GearRotation>().Activation();

        switch(m_OutPostIndex)
        {
            case 0:
                /* ADD ANTENNA */
                break; 
            case 1:
                PlayerManager.Instance.UpgradeLifeBar();
                /* ADD Cargo Visual */
                break;
            case 2:
                PlayerManager.Instance.UpgradeRessourcesBar();
                /* Upgrade Cargo Visual with another visual*/
                break;
            case 3: PlayerManager.Instance.UpgradeLifeBar(); break;
            case 4: PlayerManager.Instance.UpgradeRessourcesBar(); break;

        }
    }
}

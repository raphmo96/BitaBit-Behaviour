using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class OutpostShop : MonoBehaviour
{
    private SphereCollider m_Collider;

    [SerializeField]
    private int m_OutPostIndex;

    private float m_ShopRadius = 3.0f;

    private bool m_IsDiscovered = false;

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
        if (!m_IsDiscovered)
        {
            m_IsDiscovered = true;

            PlayerManager.Instance.SpendRessources();
            Component[] gears = GetComponentsInChildren<GearRotation>();
            foreach (GearRotation gear in gears)
                gear.Activation();

            switch (m_OutPostIndex)
            {
                case 1:
                    PlayerManager.Instance.ActivateAntenna();
                    break;
                case 2:

                    PlayerManager.Instance.NextOutpostIndex();
                    PlayerManager.Instance.UpgradeAntenna();
                    
                    break;
                case 3:

                    PlayerManager.Instance.ActivateCargo();
                    PlayerManager.Instance.UpgradeLifeBar();
                    PlayerManager.Instance.NextOutpostIndex();
                    break;
                case 4:
                    PlayerManager.Instance.ActivateCargoUpgrade();
                    PlayerManager.Instance.UpgradeRessourcesBar();
                    PlayerManager.Instance.NextOutpostIndex();
                    break;
                case 5:
                    PlayerManager.Instance.UpgradeLifeBar();
                    PlayerManager.Instance.UpgradeRessourcesBar();
                    PlayerManager.Instance.NextOutpostIndex();
                    break;
            }

        }
    }
}

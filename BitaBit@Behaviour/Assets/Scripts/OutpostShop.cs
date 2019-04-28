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

    private void OnTriggerEnter(Collider aOther)
    {
        PlayerManager player = aOther.GetComponent<PlayerManager>();
        if (player != null)
        {

            if (!m_IsDiscovered)
            {
                m_IsDiscovered = true;

                player.SpendRessources();
                Component[] gears = GetComponentsInChildren<GearRotation>();
                foreach (GearRotation gear in gears)
                    gear.Activation();

                switch (m_OutPostIndex)
                {
                    case 1:
                        player.ActivateAntenna();
                        break;
                    case 2:
                        player.NextOutpostIndex();
                        player.UpgradeAntenna();
                        break;
                    case 3:
                        player.ActivateCargo();
                        player.UpgradeLifeBar();
                        player.NextOutpostIndex();
                        break;
                    case 4:
                        player.ActivateCargoUpgrade();
                        player.UpgradeRessourcesBar();
                        player.NextOutpostIndex();
                        break;
                    case 5:
                        player.UpgradeLifeBar();
                        player.UpgradeRessourcesBar();
                        player.NextOutpostIndex();
                        break;
                }

            }
        }
    }
}

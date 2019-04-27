using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class OutpostShop : MonoBehaviour
{
    private SphereCollider m_Collider;

    [SerializeField]
    private int m_OutPostIndex;

    private float m_ShopRadius = 5.0f;

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
        if (other.gameObject.GetComponent<SpiderController>())
        {

            PlayerManager.Instance.SpendRessources();
            GetComponentInChildren<GearRotation>().Activation();

            switch (m_OutPostIndex)
            {
                case 0:
                    PlayerManager.Instance.ActivateAntenna();
                    break;
                case 1:
                    PlayerManager.Instance.UpgradeLifeBar();
                    PlayerManager.Instance.ActivateCargo();
                    PlayerManager.Instance.NextOutpostIndex();
                    PlayerManager.Instance.m_Antenna.GetComponent<Antenna>().ActivateDestination();
                    break;
                case 2:
                    PlayerManager.Instance.UpgradeRessourcesBar();
                    PlayerManager.Instance.ActivateCargoUpgrade();
                    PlayerManager.Instance.NextOutpostIndex();
                    break;
                case 3:
                    PlayerManager.Instance.UpgradeLifeBar();
                    PlayerManager.Instance.NextOutpostIndex();
                    break;
                case 4:
                    PlayerManager.Instance.UpgradeRessourcesBar();
                    PlayerManager.Instance.NextOutpostIndex();
                    break;
            }
        }
    }
}

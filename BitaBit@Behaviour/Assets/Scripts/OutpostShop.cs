using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class OutpostShop : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Player;

    [SerializeField]
    private AudioClip m_FactoryLoop;
    private AudioSource m_AudioSource;

    private bool m_PlaySFX;

    [SerializeField]
    private float m_DistanceToPlay = 250f;

    private SphereCollider m_Collider;

    [SerializeField]
    private int m_OutPostIndex;

    private float m_ShopRadius = 3.0f;

    private bool m_IsDiscovered = false;

    private void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
        m_Collider = GetComponent<SphereCollider>();
        m_Collider.radius = m_ShopRadius;
    }

    private void Update()
    {
        if(m_PlaySFX)
        {
            if(!GameManager.Instance.Player.Spend)
            {
                m_AudioSource.Stop();
                m_PlaySFX = false;
            }
        }
        else
        {
            if (GameManager.Instance.Player.Spend)
            {
                m_AudioSource.clip = m_FactoryLoop;
                m_AudioSource.Play();
                m_PlaySFX = true;
            }
        }
    }

    private void OnTriggerEnter(Collider aOther)
    {
        PlayerManager player = GameManager.Instance.Player;        

        if (player != null)
        {
           

           m_IsDiscovered = true;

           player.SpendRessources();
           Component[] gears = GetComponentsInChildren<GearRotation>();
           foreach (GearRotation gear in gears)
                    gear.Activation();

           if (!m_IsDiscovered)
           {
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

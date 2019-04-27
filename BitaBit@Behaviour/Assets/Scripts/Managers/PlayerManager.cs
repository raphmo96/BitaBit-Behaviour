using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : Singleton<PlayerManager>
{
    [SerializeField]
    private Slider m_LifeSlider;
    [SerializeField]
    private Slider m_RessourceSlider;
    [SerializeField]
    private GameObject m_Panel;
    [SerializeField]
    private Text m_Text;

    public GameObject m_Antenna;
    [SerializeField]
    private GameObject m_Cargo;
    [SerializeField]
    private GameObject m_CargoUpgrade;

    private float m_LifeValue = 1.0f;
    private float m_RessourcesValue = 0.0f;

    private float m_TransferTime = 150.0f;

    private float m_LerpValue = 0f;

    [SerializeField]
    private bool m_SpendRessources = false;

    public List<OutpostShop> m_Outposts = new List<OutpostShop>();
    private int m_OutpostIndex = 0;

    private void Start()
    {
        m_LifeSlider.value = m_LifeValue;
        m_RessourceSlider.value = m_RessourcesValue;
        m_Panel.SetActive(false);
    }

    private void Update()
    {
        if (m_SpendRessources)
        {
            m_LerpValue += Time.deltaTime / m_TransferTime;

            if (m_RessourceSlider.value > 0 && m_LifeSlider.value < m_LifeSlider.maxValue)
            {
                SetSliderValues();
            }
            else
            {
                m_SpendRessources = false;
                m_LerpValue = 0f;
            }
        }
    }

    private void SetSliderValues()
    {
        m_RessourceSlider.value -= m_LerpValue ;
        m_LifeSlider.value += m_LerpValue;
    }


    public void SpendRessources()
    {
        m_SpendRessources = true;
    }

    public void AddRessources(float ressValue)
    {
        m_RessourceSlider.value += ressValue;
    }

    public void UpgradeLifeBar()
    {
        m_LifeSlider.maxValue += 0.2f;
        m_LifeSlider.gameObject.transform.localScale += new Vector3(0.2f, 0f, 0f);
        StartCoroutine(ShowText("Life bar upgraded"));
    }

    public void UpgradeRessourcesBar()
    {
        m_RessourceSlider.maxValue += 0.2f;
        m_RessourceSlider.gameObject.transform.localScale += new Vector3(0.2f, 0f, 0f);
        StartCoroutine(ShowText("Ressource bar upgraded"));
    }    

    private IEnumerator ShowText(string text)
    {
        m_Panel.SetActive(true);
        m_Text.text = text;

        yield return new WaitForSeconds(3.0f);
        m_Panel.SetActive(false);
    }

    public void ActivateAntenna()
    {
        m_Antenna.SetActive(true);
    }

    public void ActivateCargo()
    {
        m_Cargo.SetActive(true);
    }

    public void ActivateCargoUpgrade()
    {
        m_CargoUpgrade.SetActive(true);
    }

    public void NextOutpostIndex()
    {
        m_OutpostIndex++;
    }

    public int GetOutpostIndex()
    {
        return m_OutpostIndex;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private Slider m_LifeSlider;
    [SerializeField]
    private Slider m_RessourceSlider;
    [SerializeField]
    private GameObject m_Panel;
    [SerializeField]
    private TextMeshProUGUI m_Text;

    public GameObject m_Antenna;
    [SerializeField]
    private GameObject m_Cargo;
    [SerializeField]
    private GameObject m_CargoUpgrade;
    [SerializeField]
    private GameObject m_DeathPanel;

    private float m_LifeValue = 1.0f;
    private float m_RessourcesValue = 0.0f;

    private bool m_IsDead = false;

    private float m_TransferTime = 1000.0f;

    private float m_LerpValue = 0f;

    [SerializeField]
    private bool m_SpendRessources = false;
    public bool Spend
    {
        get { return m_SpendRessources; }
    }

    public List<OutpostShop> m_Outposts = new List<OutpostShop>();
    private int m_OutpostIndex = 0;

    private void Start()
    {
        m_LifeSlider.value = m_LifeValue * 0.5f;
        m_RessourceSlider.value = m_RessourcesValue;
        m_DeathPanel.SetActive(false);
        m_Panel.SetActive(false);
        m_Antenna.SetActive(false);
        m_Cargo.SetActive(false);
        m_CargoUpgrade.SetActive(false);
        GameManager.Instance.Player = this;
        AudioManager.Instance.PlayMusic("Game");
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

        if (m_LifeSlider.value <= 0)
        {       
            if (!m_IsDead)
            {
                m_IsDead = true;
                m_DeathPanel.SetActive(true);
                StartCoroutine(DeathCoroutine());
            }
        }
    }

    private void SetSliderValues()
    {
        m_RessourceSlider.value -= m_LerpValue;
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
        StartCoroutine(ShowText("Life bar Upgraded"));
    }

    public void UpgradeRessourcesBar()
    {
        m_RessourceSlider.maxValue += 0.2f;
        m_RessourceSlider.gameObject.transform.localScale += new Vector3(0.2f, 0f, 0f);
        StartCoroutine(ShowText("Ressource bar Upgraded"));
    }

    private IEnumerator DeathCoroutine()
    {
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(ShowText("Restarting in 5"));
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(ShowText("Restarting in 4"));
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(ShowText("Restarting in 3"));
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(ShowText("Restarting in 2"));
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(ShowText("Restarting in 1"));
        m_DeathPanel.SetActive(false);
        yield return new WaitForSeconds(1.0f);
        // Reload scene
        SceneLoadingManager.Instance.ChangeScene(EScenes.Game);
    }

    private IEnumerator ShowText(string text)
    {
        m_Panel.SetActive(true);
        m_Text.text = text;

        yield return new WaitForSeconds(1.0f);
        m_Panel.SetActive(false);
    }

    public void UpgradeAntenna()
    {
        StartCoroutine(ShowText("Antenna Upgraded"));
        m_Antenna.GetComponent<Antenna>().ActivateDestination();
    }

    public void ActivateAntenna()
    {
        m_Antenna.SetActive(true);
        StartCoroutine(ShowText("Antenna Activated"));
    }

    public void ActivateCargo()
    {
        m_Cargo.SetActive(true);
        StartCoroutine(ShowText("Cargo Built"));
    }

    public void ActivateCargoUpgrade()
    {
        m_CargoUpgrade.SetActive(true);
        StartCoroutine(ShowText("Cargo Upgraded"));
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

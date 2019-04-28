using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Consumables : MonoBehaviour
{
    [SerializeField]
    private AudioClip m_SFX;
    private void OnTriggerEnter(Collider aOther)
    {
        AudioManager.Instance.PlaySFX(m_SFX, transform.localPosition);
        GameManager.Instance.Player.AddRessources(0.1f);
        Destroy(gameObject);
    }
}

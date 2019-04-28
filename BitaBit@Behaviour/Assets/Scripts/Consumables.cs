using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Consumables : MonoBehaviour
{    

    private void OnTriggerEnter(Collider aOther)
    {
        GameManager.Instance.Player.AddRessources(0.1f);
        Destroy(gameObject);
    }
}

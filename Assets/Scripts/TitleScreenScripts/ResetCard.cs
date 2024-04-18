using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ResetCard : MonoBehaviour
{
    [SerializeField] SpawnManager manager;
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "card")
        {
            other.transform.position = manager.standbyTransfrom.position;
            
            manager.spawnableCards.Add(other.gameObject);
        }
    }
}

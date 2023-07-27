using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class healthKit : NetworkBehaviour
{ 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {           
            other.GetComponent<Player>().GetHealth.health.Value += 2;
            GetComponent<NetworkObject>().Despawn();            
        }
    }       

    public override void OnNetworkSpawn()
    {
        print("Spawn a HealthKit");
    }

}

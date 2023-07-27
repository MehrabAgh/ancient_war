using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class ItemManager : NetworkBehaviour
{
    public static ItemManager Instanse { get; set; }
    public healthKit ObjHealth, _healthKit;

    private void Awake()
    {
        if (!Instanse)
            Instanse = this;
        else
            DontDestroyOnLoad(this);
    }

    private void Start()
    {
        if (!_healthKit) Spawn();
    }
   
    IEnumerator NextSpawn(float delay)
    {
        yield return new WaitForSeconds(delay);
        Spawn();
    }
    public GameObject Spawn()
    {
        GameObject h = null;
        if (!h)
        {
            h = Instantiate(ObjHealth.gameObject);
            h.GetComponent<NetworkObject>().Spawn();
            return h;
        }
        return h;
    }

    private void CheckHealthKit() => _healthKit = FindObjectOfType<healthKit>();
}


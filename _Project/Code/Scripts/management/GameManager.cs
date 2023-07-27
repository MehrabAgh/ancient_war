using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class GameManager : NetworkBehaviour
{
    public static GameManager instanse;    

    private void Awake()
    {
        if (!instanse)
            instanse = this;
        else DontDestroyOnLoad(this);
    }

    //private void Start()
    //{
    //    StartCoroutine(Loop(0.1f));
    //}
    //private IEnumerator Loop(float timeFrame)
    //{
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(timeFrame);
            
    //    }
    //}
}

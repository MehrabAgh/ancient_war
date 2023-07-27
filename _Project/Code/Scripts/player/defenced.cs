using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class defenced : NetworkBehaviour
{
   
    private bool _active;

    [SerializeField]private GameObject myArmor;

    void Update()
    {
        myArmor.SetActive(_active);

       

        ProccesActive();      
    }

    private void ProccesActive()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)) SetActive(true);

        if (Input.GetKeyUp(KeyCode.LeftShift)) SetActive(false);
    }

    void SetActive(bool _isActive) => _active = _isActive;
  
}

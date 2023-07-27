using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Health : NetworkBehaviour
{

    public NetworkVariable<int> health = new NetworkVariable<int>
        (2 , NetworkVariableReadPermission.Everyone , NetworkVariableWritePermission.Server);

    public int maxHealth;
    private Animator GetAnimator;
    private void Awake()
    {
        GetAnimator = GetComponent<Animator>();
        health.Value = maxHealth;
    }

    public override void OnNetworkSpawn()
    {             
        health.OnValueChanged += OnHealthChanged;
    }

    public void OnHealthChanged(int previous, int current)
    {
        if (current <= 0)
        {
            
            GetAnimator.SetInteger("_IndexDeath", 1);
            Invoke(nameof(CoolDown), 1);          
        }
    }

    private void CoolDown()=> gameObject.SetActive(false);

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("PlayerSword")) health.Value--;
    }
}

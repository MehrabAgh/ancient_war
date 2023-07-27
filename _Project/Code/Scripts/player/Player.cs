using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Player : NetworkBehaviour
{
    [HideInInspector] public Health GetHealth;
    [HideInInspector] public attack GetAttack;
    
    [SerializeField] private AnimatorOverrideController MyAnimCollection;

    [HideInInspector] public Animator GetAnimator;
    [HideInInspector] public Transform GetTransform;

    NetworkVariable<uint> playerId = new NetworkVariable<uint>();
    public uint PlayerId { get => playerId.Value; set => playerId.Value = value; }

    [SerializeField] private CharacterManager CM;
    private void Awake()
    {
        GetAnimator = GetComponent<Animator>();
        GetTransform = GetComponent<Transform>();
        GetAttack = GetComponent<attack>();
        GetHealth = GetComponent<Health>();
        CM = FindObjectOfType<CharacterManager>();
        SetAnimation(MyAnimCollection);
    }

    private void Start()
    {      
        StartCoroutine(LoopAttack(0.05f));
    }

    private IEnumerator LoopAttack(float timeframe)
    {
        while (true)
        {
            yield return new WaitForSeconds(timeframe);          
            if (GetAttack._attacking)
                GetAttack.Attack_Logic(this);
        }

    }
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        print("New Player");
        CM.AddingPlayer(this);        
    }   


    public void SetAnimation(AnimatorOverrideController animatorOverride)
    {
        GetAnimator.runtimeAnimatorController = animatorOverride;
    }

}

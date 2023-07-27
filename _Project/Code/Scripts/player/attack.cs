using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class attack : NetworkBehaviour
{
    private bool _isAttack { get { return _attacking; } set { _attacking = value; } }
    public bool _attacking;
    private int _indexLayer , _indexAttack;

    private Animator GetAnimator;
    private void Awake()=> GetAnimator = GetComponent<Animator>();

    public void Attack_Logic(Player player)
    {
        _indexAttack = Random.Range(1, 3);
        player.GetAnimator.SetLayerWeight(1, _indexLayer);
        GetAnimator.SetInteger("_IndexAttack", _indexAttack);        
    }


    public void Attack_On()
    {
        _isAttack = true;
        _indexLayer = 1;
    }
    public void Attack_Off()
    {
        _isAttack = false;
        _indexAttack = 0;
        _indexLayer = 0;
        GetAnimator.SetInteger("_IndexAttack", _indexAttack);
    }
}

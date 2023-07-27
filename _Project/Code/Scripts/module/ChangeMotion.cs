using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMotion : MonoBehaviour
{
    [SerializeField] private AnimatorOverrideController[] overrideControllers;
    [SerializeField] private Player overrider;

    public void Set(int value)
    {        
        overrider.SetAnimation(overrideControllers[value]);
    }
}

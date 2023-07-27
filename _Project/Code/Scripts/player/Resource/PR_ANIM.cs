using UnityEngine;

[CreateAssetMenu(fileName = "AnimateAttack", menuName = "Resources/AnimateAttack", order = 1)]
public class PR_ANIM : ScriptableObject
{
    public AnimationClip[] AttackClips;
    public AnimationClip[] DamageClips;
    public AnimationClip[] DeathClips;
}

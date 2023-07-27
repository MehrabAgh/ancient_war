using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UISys_InGame : MonoBehaviour
{
    [SerializeField] private EventTrigger FireAttack;

    void Start()
    {       
        EventTrigger.Entry entry1 = new EventTrigger.Entry();

        entry1.eventID = EventTriggerType.PointerUp;
        entry1.callback.AddListener((data) => { 
            CharacterManager.instanse.GettingPlayer(2).GetAttack.Attack_Off(); });
        FireAttack.triggers.Add(entry1);

        EventTrigger.Entry entry2 = new EventTrigger.Entry();
        entry2.eventID = EventTriggerType.PointerDown;
        entry2.callback.AddListener((data) => {
            CharacterManager.instanse.GettingPlayer(2).GetAttack.Attack_On(); });
        FireAttack.triggers.Add(entry2);
    }

}

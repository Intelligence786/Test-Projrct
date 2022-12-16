using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealAction : ActionBase
{
    [SerializeField]
    private int healAmount = 3;
    protected override void HandleAction(Character character)
    {
        if (currentCharacter == character)
        {
            character.health.ModifyHealth(healAmount);
            DestroyAction();
        }
        else
            base.HandleAction(character);
    }
}

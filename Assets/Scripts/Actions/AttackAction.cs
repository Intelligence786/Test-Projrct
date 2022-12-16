using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : ActionBase
{
    [SerializeField]
    private int damageAmount = 3;
    protected override void HandleAction(Character character)
    {
        if (currentCharacter.type != character.type)
        {
            character.Damage(damageAmount);
            DestroyAction();
        }
        else
            base.HandleAction(character);
    }
}

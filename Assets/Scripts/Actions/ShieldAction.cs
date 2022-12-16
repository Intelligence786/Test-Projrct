using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldAction : ActionBase
{
    [SerializeField]
    private int shieldAmount = 3;
    [SerializeField]
    private int activeTurn = 1;

    private Character character = null;
    protected override void HandleAction(Character character)
    {
        if (currentCharacter.type == character.type)
        {
            Debuff debuff = new Debuff(activeTurn, shieldAmount);
            character.health.AdditionalHealth = shieldAmount;
            character.health.ModifyHealth();
            character.Debuff(debuff);
            DestroyAction();
        }
        else
            base.HandleAction(character);
    }

}

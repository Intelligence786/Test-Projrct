using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonAction : ActionBase
{
    [SerializeField]
    private int damageAmount = 1;
    [SerializeField]
    private int poisionAmount = 1;
    [SerializeField]
    private int activeTurn = 1;
    protected override void HandleAction(Character character)
    {
        if (currentCharacter.type != character.type)
        {
            Debuff debuff = new Debuff(poisionAmount, activeTurn);
            character.Damage(damageAmount);

            debuff.onDebuffUse += character.Damage;

            character.Debuff(debuff);
            DestroyAction();
        }
        else
            base.HandleAction(character);
    }
}

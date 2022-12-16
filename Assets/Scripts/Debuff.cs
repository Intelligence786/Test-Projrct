using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debuff
{
    public int debuffAmount = 0;
    public int debuffTurnCount = 0;

    public Debuff(int debuffAmount, int debuffTurnCount)
    {
        this.debuffAmount = debuffAmount;
        this.debuffTurnCount = debuffTurnCount;
    }

    public event Action<int> onDebuffUse = delegate { };

    public void DebuffUse()
    {
        if (debuffTurnCount > 0)
        {
            onDebuffUse(debuffAmount);
            debuffTurnCount--;
        }
        else
        {
            GC.SuppressFinalize(this);
        }
    }
}

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
    public event Action onDebuffEnd = delegate { };
    public void DebuffUse()
    {
        onDebuffUse(debuffAmount);
        debuffTurnCount--;

        if (debuffTurnCount > 0)
        {
            onDebuffEnd();
            GC.SuppressFinalize(this);
        }
    }
}

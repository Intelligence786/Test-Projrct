using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public enum CharacterType
{
    Friend,
    Enemy
}

public abstract class Character : MonoBehaviour
{
    private bool stillAlive = true;
    public Health health;


    public List<Debuff> debuffsList = new List<Debuff>();

    public CharacterType type;

    public bool StillAlive { get => stillAlive; }

    public event Action OnDeath = delegate { };

    private void Awake()
    {
        health = GetComponentInChildren<Health>();
        health.OnDeath += HandleOnDeath;
    }

    public void YourTurn()
    {
        if (debuffsList.Count > 0)
        {
            foreach (Debuff debuff in debuffsList)
            {
                debuff.DebuffUse();
            }
        }
    }

    private void HandleOnDeath()
    {
        if (StillAlive)
            Death();
    }

    public void Damage(int damageCount)
    {
        if (StillAlive)
            health.ModifyHealth(-damageCount);
    }

    protected void Death()
    {
        Debug.Log("Death");
        stillAlive = false;
        OnDeath();
    }

    public void Debuff(Debuff debuff)
    {
        debuffsList.Add(debuff);
    }
}
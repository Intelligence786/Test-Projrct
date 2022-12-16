using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 10;
    [SerializeField]
    private int currentHealth = 0;
    [SerializeField]
    private int additionalHealth = 0;

    public int AdditionalHealth { get => additionalHealth; set => additionalHealth = value; }

    public event Action<int, int> OnHealthChanged = delegate { };
    public event Action OnDeath = delegate { };


    public void ModifyHealth(int amount = 0)
    {
        if (amount < 0 && AdditionalHealth > 0)
        {
            AdditionalHealth += amount;
        }
        else
            currentHealth += amount;

        if (currentHealth > 0)
            OnHealthChanged(currentHealth, AdditionalHealth);
        else
        {
            OnHealthChanged(0, 0);
            OnDeath();
        }
    }

    internal void UpdateUIHealth(Action<int, int> handleOnHealthChanged)
    {
        OnHealthChanged += handleOnHealthChanged;
        currentHealth = maxHealth;
        ModifyHealth();
    }
}

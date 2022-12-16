using Unity;
using UnityEngine;
using System;
using TMPro;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private TMP_Text healthText;

    private void Awake()
    {
        GetComponentInParent<Health>().UpdateUIHealth(HandleOnHealthChanged);
    }

    private void HandleOnHealthChanged(int amount, int additionalAmount)
    {
        if (additionalAmount > 0)
            healthText.text = $"{amount} + {additionalAmount} HP";
        else
            healthText.text = $"{amount} HP";
    }


}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public void Attack()
    {

    }

    IEnumerator OnAttack()
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
        }
    }
}
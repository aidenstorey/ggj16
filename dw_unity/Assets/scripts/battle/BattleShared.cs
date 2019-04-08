using UnityEngine;
using System.Collections;

public class BattleShared : MonoBehaviour
{
    public int MaxHealth = 10;
    public int CurrentHealth;
    public int Damage = 3;

    void Start()
    {
        CurrentHealth = MaxHealth;
    }

    public bool TakeDamage( int _DamageAmount )
    {
        CurrentHealth -= _DamageAmount;

        if( CurrentHealth <= 0 )
        {
            // TODO: Death
            return true;
        }

        return false;
    }
}

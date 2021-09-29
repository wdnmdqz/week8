using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    public int maxHealth = 50;
    private int health;
    public bool IsDead
    {
        get
        {
            return health <= 0;
        }
    }

    private int shield = 0;
    private int strength = 0;
    public int Strength
    {
        get
        {
            return strength;
        }
    }


    void Start()
    {
        health = maxHealth;
        shield = 0;
    }

    public bool Hit(int damage)
    {
        // shield blocks damage
        if (shield > damage)
        {
            shield -= damage;
            damage = 0;
        }
        else if (shield > 0)
        {
            damage -= shield;
            shield = 0;
        }

        health -= damage;
        if (health <= 0)
        {
            // Dead
            health = 0;
            return true;
        }
        else 
        {
            // Still alive
            return false;
        }
    }

    public void StartTurn()
    {
        // shield goes away at the start of the turn
        shield = 0;
    }

    public void EndTurn()
    {
        // strength goes down at the end of the turn
        if (strength > 0)
        {
            strength--;
        }
    }

    public void Heal(int points)
    {
        health += points;
        if (health > maxHealth) 
        {
            health = maxHealth;
        }
    }

    public void AddShield(int points)
    {
        shield += points;
    }

    public void AddStrength(int points)
    {
        strength += points;
    }

}

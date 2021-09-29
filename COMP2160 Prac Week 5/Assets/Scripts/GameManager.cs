using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Creature player;
    public Creature enemyPrefab;
    public Transform enemyPosition;

    public int nEnemies = 2;
    public float enemySeparation = 1.5f;
    private List<Creature> enemies;

    public int nActionsPerTurn = 3;
    private int actionsRemaining;

    public int baseStrikeDamage = 5;
    public int bonusStrikeDamage = 2;    
    public int healAmount = 2;
    public int shieldAmount = 4;
    public int strengthenAmount = 1;



    void Start()
    {        
        CreateEnemies();
        StartTurn();
    }

    private void CreateEnemies()
    {
        enemies = new List<Creature>();
        for (int i = 0; i < nEnemies; i++)
        {
            Creature enemy = Instantiate(enemyPrefab);
            enemy.transform.parent = enemyPosition;
            enemy.transform.localPosition = new Vector3(i * enemySeparation, 0, 0);
            enemies.Add(enemy);
        }
    }

    private void StartTurn()
    {
        if (player.IsDead) 
        {
            actionsRemaining = 0;
        }
        else
        {
            actionsRemaining = nActionsPerTurn;
        }

        // broadcast to player and enemies
        player.StartTurn();
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].StartTurn();
        }
    }

    public void EndTurn()
    {
        // broadcast to player and enemies
        player.EndTurn();

        // enemies attack the player
        for (int i = 0; i < enemies.Count; i++)
        {
            int damage = baseStrikeDamage + bonusStrikeDamage * player.Strength;
            player.Hit(damage);
            enemies[i].EndTurn();
        }

        // start the next turn
        StartTurn();
    }

    public void DoStrike()
    {
        // for simplicity, we'll only attack the first enemy
        if (actionsRemaining > 0)
        {
            actionsRemaining--;
            int damage = baseStrikeDamage + bonusStrikeDamage * player.Strength;
            bool dead = enemies[0].Hit(damage);
            if (dead)
            {
                Destroy(enemies[0].gameObject);
                enemies.RemoveAt(0);
            }
        }
    }

    public void DoHeal()
    {
        if (actionsRemaining > 0)
        {
            actionsRemaining--;
            player.Heal(healAmount);
        }
    }

    public void DoShield()
    {
        if (actionsRemaining > 0)
        {
            actionsRemaining--;
            player.AddShield(shieldAmount);
        }
    }

    public void DoStrengthen()
    {
        if (actionsRemaining > 0)
        {
            actionsRemaining--;
            player.AddStrength(strengthenAmount);
        }
    }
}

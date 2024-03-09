using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Range(0, 200)] public int health = 100, currentHealth;

    public static bool isDead;

    private void Start()
    {
        currentHealth = health;
        isDead = false;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("You are dead");
        // set isDead to true
        isDead = true;

    }
}

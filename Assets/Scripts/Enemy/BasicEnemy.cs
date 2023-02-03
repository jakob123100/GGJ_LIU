using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    [SerializeField] private int maxHealth;

    private int currentHealth;

    private bool CheckIfDead()
    {
        return currentHealth <= 0;
    }

    private void Kill()
    {
        Destroy(gameObject);
    }

    public void TakeDamange(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (CheckIfDead())
        {
            Kill();
        }
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }
}

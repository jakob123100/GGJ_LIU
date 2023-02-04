using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    [SerializeField] int maxHealth = 10;

	[SerializeField] int health;

    private bool IsDead()
    {
        return health <= 0;
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if(IsDead() )
        {
            Die();
        }
    }

	private void Start()
	{
        health = maxHealth;
	}
}

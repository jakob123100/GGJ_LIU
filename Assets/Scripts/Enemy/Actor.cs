using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public int MaxHealth = 10;

	[SerializeField] protected int health;

	protected bool IsDead()
    {
        return health <= 0;
    }

	protected void Die()
    {
        GameController.Instance.EnemyKilled();
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

	protected void Start()
	{
        health = MaxHealth;
	}
}

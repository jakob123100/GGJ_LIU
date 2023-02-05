using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public int MaxHealth = 10;

	[SerializeField] protected int health;
    [SerializeField] private GameObject mat;
    private float currentTime;
    [SerializeField] private float flashTime;

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
        if(mat != null)
        {
            currentTime = Time.time + flashTime;
            health -= damage;
            mat.GetComponent<Renderer>().material.color = Color.red;
        }

        if (IsDead() )
        {
            Die();
        }
    }

	protected void Start()
	{
        health = MaxHealth;
    }

    private void Update()
    {
        if (mat != null)
        {
            if (Time.time > currentTime)
            {
                mat.GetComponent<Renderer>().material.color = Color.white;
            }
        }
    }
}

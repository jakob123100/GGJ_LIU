using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public int MaxHealth = 10;

	[SerializeField] protected int health;
    [SerializeField] protected GameObject mat;
	protected float currentTime;
    [SerializeField] protected float flashTime;

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

		if (mat != null)
        {
            currentTime = Time.time + flashTime;
            mat.GetComponent<Renderer>().material.color = Color.red;
        }

        if (IsDead() )
        {
            Die();
        }
    }

	virtual protected void Start()
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

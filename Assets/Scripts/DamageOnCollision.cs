using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnCollision : MonoBehaviour
{
	[SerializeField] private int damage = 1;
	[SerializeField] private float damageCD = 0.5f;

	private float lastCollisionTime = 0f;

	private void OnCollisionStay(Collision collision)
	{
		Actor other = collision.gameObject.GetComponent<Actor>();

		if (other == null)
		{
			return;
		}

		if (Time.time - lastCollisionTime > damageCD && other.gameObject.tag == "Player")
		{
			lastCollisionTime = Time.time;
			other.TakeDamage(damage);
		}
	}
}

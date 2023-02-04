using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    Vector3 direction;
    public int damage;
    GameObject parent;

    public static void CreateComponent(GameObject gameObject, float speed, Vector3 direction, int damage, GameObject parent = null, float destroyDelay = 3f)
    {
        Bullet component = gameObject.AddComponent<Bullet>();
        component.speed = speed;
        component.direction = direction;
        component.damage = damage;
        component.parent = parent;
        Destroy(gameObject, destroyDelay);
    }

    public void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

	private void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject == parent || collision.gameObject.GetComponent<Bullet>() != null)
        {
            return;
		}

		Destroy(gameObject);

		Actor other = collision.gameObject.GetComponent<Actor>();
        if(other == null) { return; }
        other.TakeDamage(damage);
	}
}

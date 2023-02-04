using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    Vector3 direction;
    public int damage;
    GameObject parent;
    AnimationCurve speedOverLifetime;
    float startTime = 0f;
    float lifetime;


	public static void CreateComponent(GameObject gameObject, float speed, Vector3 direction, int damage, float scale, GameObject parent = null, float destroyDelay = 3f, AnimationCurve speedOverLifetime = null)
    {
        Bullet component = gameObject.AddComponent<Bullet>();
        component.speed = speed;
        component.direction = direction;
        component.damage = damage;
        component.parent = parent;
        component.startTime = Time.time;
        component.speedOverLifetime = speedOverLifetime;
        component.lifetime = destroyDelay;
        gameObject.transform.localScale = scale * Vector3.one;
        Destroy(gameObject, destroyDelay);
    }

    public void Update()
    {
        float stepSize = speed * Time.deltaTime;
        lifetime += Time.deltaTime;

        if(speedOverLifetime != null)
        {
            stepSize *= speedOverLifetime.Evaluate((Time.time - startTime) / lifetime);
        }

        transform.position += direction * stepSize;
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

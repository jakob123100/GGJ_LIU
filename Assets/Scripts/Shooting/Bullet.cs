using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public int damage;
    Vector3 direction;
    string parentFaction;
    AnimationCurve speedOverLifetime;
    float startTime = 0f;
    float lifetime;


	public static void CreateComponent(GameObject gameObject, float speed, Vector3 direction, int damage, float scale, string parent = null, float destroyDelay = 3f, AnimationCurve speedOverLifetime = null)
    {
        Bullet component = gameObject.AddComponent<Bullet>();
        component.speed = speed;
        component.direction = direction.normalized;
        component.damage = damage;
        component.parentFaction = parent;
        component.startTime = Time.time;
        component.speedOverLifetime = speedOverLifetime;
        component.lifetime = destroyDelay;
        gameObject.transform.localScale = scale * Vector3.one;
        Destroy(gameObject, destroyDelay);
    }

    public void FixedUpdate()
    {
        float stepSize = speed * Time.fixedDeltaTime;
        lifetime += Time.fixedDeltaTime;

        if(speedOverLifetime != null)
        {
            stepSize *= speedOverLifetime.Evaluate((Time.time - startTime) / lifetime);
        }

        transform.position += direction * stepSize;
    }

	private void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == parentFaction || collision.gameObject.GetComponent<Bullet>() != null)
        {
            return;
		}

		Destroy(gameObject);

		Actor other = collision.gameObject.GetComponent<Actor>();
        if(other == null) { return; }
        other.TakeDamage(damage);

        if(other != null)
		{
			other.TakeDamage(damage);

            Vector3 collisionPoint = collision.GetContact(0).point;
            DamagePopupSpawner.Create(collisionPoint, damage);
			return; 
        }

		/*PlayerActor otherActor = collision.gameObject.GetComponent<PlayerActor>();
		if (other != null)
		{
            Debug.Log("asdasd");
			otherActor.TakeDamage(damage);
			return;
		}*/
	}
}

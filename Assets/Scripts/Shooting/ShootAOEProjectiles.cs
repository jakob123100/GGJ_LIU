using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAOEProjectiles : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private AnimationCurve speedOverLifetime;
    [SerializeField] private Vector3 bulletSpawnOffset = Vector3.zero;

    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float scale = 1f;
    [SerializeField] private float bulletDestroyDelay = 3f;
    [SerializeField] private float bulletSpread = 10f;

    [SerializeField] private int amountOfBullts = 5;
    [SerializeField] private int bulletDamage = 3;

    private float timeUntilNextBullet = 0;
    private Vector3[] directions;

    private void GenerateDirections()
    {
        float angleStepSize = 360 / (amountOfBullts);
        directions = new Vector3[amountOfBullts];

        for (int i = 0; i < amountOfBullts; i++)
        {
            float angle = i * angleStepSize;
            Vector3 direction = new Vector3(
                Mathf.Sin(Mathf.Deg2Rad * angle),
                0,
                Mathf.Cos(Mathf.Deg2Rad * angle));
            directions[i] = direction;
        }
    }

    private void FireBullets()
    {
        if (amountOfBullts <= 0) { return; }

        for (int i = 0; i < amountOfBullts; i++)
        {
            float spreadDirection = Random.Range(-bulletSpread, bulletSpread);

            Vector3 direction = directions[i];
            direction = Quaternion.Euler(0, spreadDirection, 0) * direction;

            GameObject bullet = Instantiate(
                bulletPrefab,
                transform.position + bulletSpawnOffset,
                Quaternion.LookRotation(direction));

            Bullet.CreateComponent(
                bullet,
                bulletSpeed,
                direction,
                bulletDamage,
                scale,
                gameObject.tag,
                bulletDestroyDelay,
                speedOverLifetime);
        }
    }

    void Awake()
    {
        GenerateDirections();
    }

    void FixedUpdate()
    {
        if (Time.time > timeUntilNextBullet)
        {
            timeUntilNextBullet = Time.time + fireRate;
            FireBullets();
        }
    }
}

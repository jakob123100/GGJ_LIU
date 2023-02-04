using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAOEProjectiles : MonoBehaviour
{
    [SerializeField] private int amountOfBullts = 5;
    [SerializeField] private int bulletDamage = 3;
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float scale = 1f;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletDestroyDelay = 3f;
    [SerializeField] private Vector3 bulletSpawnOffset = Vector3.zero;
    [SerializeField] private AnimationCurve speedOverLifetime;

    [SerializeField] private float bulletSpread = 10f;

    private float timeUntilNextBullet = 0;

    private void FireBullets()
    {
        if(amountOfBullts <= 0) { return; }

        float angleStepSize = 360 / (amountOfBullts);
        

        for (int i = 0; i < amountOfBullts; i++)
        {
            float spreadDirection = Random.Range(-bulletSpread, bulletSpread);

            float angle = i * angleStepSize;
            Vector3 direction = new(
                Mathf.Sin(Mathf.Deg2Rad * angle + spreadDirection) , 
                0, 
                Mathf.Cos(Mathf.Deg2Rad * angle + spreadDirection));

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
                gameObject, 
                bulletDestroyDelay,
                speedOverLifetime);
        }
    }

    void Update()
    {
        if(Time.time > timeUntilNextBullet)
        {
            timeUntilNextBullet  = Time.time + fireRate;
            FireBullets();
        }
    }
}

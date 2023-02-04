using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAOEProjectiles : MonoBehaviour
{
    [SerializeField] int amountOfBullts = 5;
    [SerializeField] int bulletDamage = 3;
    [SerializeField] float bulletSpeed = 5f;
    [SerializeField] float fireRate = 1f;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletDestroyDelay = 3f;
    [SerializeField] Vector3 bulletSpawnOffset = Vector3.zero;

    float lastFireTime = 0;

    private void FireBullets()
    {
        if(amountOfBullts <= 0) { return; }

        float angleStepSize = 360 / (amountOfBullts);

        for (int i = 0; i < amountOfBullts; i++)
        {
            float angle = i * angleStepSize;
            Vector3 direction = new Vector3(Mathf.Sin(Mathf.Deg2Rad * angle), 0, Mathf.Cos(Mathf.Deg2Rad * angle));

            GameObject bullet = Instantiate(bulletPrefab, transform.position + bulletSpawnOffset, Quaternion.LookRotation(direction));
            Bullet.CreateComponent(bullet, bulletSpeed, direction, bulletDamage, gameObject, bulletDestroyDelay);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - lastFireTime > fireRate)
        {
            lastFireTime= Time.time;
            FireBullets();
        }
    }
}

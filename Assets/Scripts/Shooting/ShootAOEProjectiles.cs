using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAOEProjectiles : MonoBehaviour
{
    [SerializeField] int amountOfBullts = 5;
    [SerializeField] int bulletDamage = 3;
    [SerializeField] float bulletSpeed = 5f;
    [SerializeField] float fireRate = 1f;
    [SerializeField] float scale = 1f;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletDestroyDelay = 3f;
    [SerializeField] Vector3 bulletSpawnOffset = Vector3.zero;
    [SerializeField] AnimationCurve speedOverLifetime;

    [SerializeField] private float bulletSpread;

    float lastFireTime = 0;

    private void FireBullets()
    {
        if(amountOfBullts <= 0) { return; }

        float angleStepSize = 360 / (amountOfBullts);
        

        for (int i = 0; i < amountOfBullts; i++)
        {
            float spreadDirection = Random.Range(-bulletSpread, bulletSpread);

            float angle = i * angleStepSize;
            Vector3 direction = new Vector3(
                Mathf.Sin(Mathf.Deg2Rad * angle + spreadDirection) , 
                0, 
                Mathf.Cos(Mathf.Deg2Rad * angle + spreadDirection));

            GameObject bullet = Instantiate(bulletPrefab, transform.position + bulletSpawnOffset, Quaternion.LookRotation(direction));
            Bullet.CreateComponent(bullet, bulletSpeed, direction, bulletDamage, scale, gameObject, bulletDestroyDelay, speedOverLifetime);
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

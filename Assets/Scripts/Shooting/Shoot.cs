using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private GameObject objectToShoot;
    [SerializeField] private float shootingForce = 50f;
    [SerializeField] private float fireRate = 0.5f;
    [SerializeField] private float bulletSpread = 0f;
    [SerializeField] private int damage;
    [SerializeField] private float scale = 1f;

    private float currentBulletSpread;
    [SerializeField] private float bulletSpreadTime = 0f;

    private float timeUntilNextBullet;

    private void DoTheShoot()
    {
        if (Time.time > timeUntilNextBullet)
        {
            timeUntilNextBullet = Time.time + fireRate;

            if(currentBulletSpread < bulletSpread)
            currentBulletSpread += bulletSpreadTime * Time.deltaTime;

            float direction = Random.Range(-currentBulletSpread, currentBulletSpread);
            Vector3 directionVector = transform.forward + transform.right * direction;

            GameObject bullet = Instantiate(
                objectToShoot,
                transform.position,
                Quaternion.LookRotation(directionVector));

            Bullet.CreateComponent(bullet, shootingForce, directionVector, damage, scale);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            DoTheShoot();
        }
        else if (currentBulletSpread > 0)
        {
            currentBulletSpread -= bulletSpreadTime * 2 * Time.deltaTime;
        }
        else
        {
            currentBulletSpread = 0;
        }
    }
}

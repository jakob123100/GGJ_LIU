using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private GameObject objectToShoot;
    [SerializeField] private float shootingForce = 50f;
    [SerializeField] private float fireRate = 0.5f;
    private float timeUntilNextBullet;

    private void DoTheShoot()
    {
        if (Time.time > timeUntilNextBullet)
        {
            timeUntilNextBullet = Time.time + fireRate;

            GameObject bullet = Instantiate(
                objectToShoot,
                transform.position + transform.forward,
                transform.rotation);

            bullet.GetComponent<Rigidbody>()
                .AddForce(transform.forward * shootingForce, ForceMode.Impulse);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            DoTheShoot();
        }
    }
}

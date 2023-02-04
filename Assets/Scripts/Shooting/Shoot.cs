using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private GameObject objectToShoot;
    [SerializeField] private GameObject flash;
    [SerializeField] private CharacterControls movementScript;
    [SerializeField] private AnimationCurve bulletSpeedOverLifetimeCurve_IAmGoodAtNamingThings;

    [SerializeField] private float shootingForce = 50f;
    [SerializeField] private float fireRate = 0.5f;
    [SerializeField] private float bulletSpread = 0f;
    [SerializeField] private float scale = 1f;
    [SerializeField] private float knockback = 10f;
    [SerializeField] private float bulletSpreadTime = 0f;

    [SerializeField] private int damage;

    private float currentBulletSpread;
    private float timeUntilNextBullet;

    private Vector3 directionVector;

    private void Start()
    {
        directionVector = transform.forward;
    }

    private void DoTheShoot()
    {
        currentBulletSpread = Mathf.Min(currentBulletSpread + bulletSpreadTime * Time.deltaTime, bulletSpread);
        float direction = Random.Range(-currentBulletSpread, currentBulletSpread);
        directionVector = transform.forward + transform.right * direction;

        flash.SetActive(true);

        GameObject bullet = Instantiate(
            objectToShoot,
            transform.position,
            Quaternion.LookRotation(directionVector));

        Bullet.CreateComponent(bullet, shootingForce, directionVector, damage, scale, speedOverLifetime: bulletSpeedOverLifetimeCurve_IAmGoodAtNamingThings);

        movementScript.gameObject.transform.position += new Vector3(Time.deltaTime * knockback * -transform.forward.x, 0, Time.deltaTime * knockback * -transform.forward.z);
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0) && Time.time > timeUntilNextBullet && movementScript.allowedToMove)
        {
            timeUntilNextBullet = Time.time + fireRate;
            DoTheShoot();
        }
        else
        {
            currentBulletSpread = Mathf.Max(currentBulletSpread - bulletSpreadTime * 2 * Time.deltaTime, 0);
        }
    }
}

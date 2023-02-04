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
    [SerializeField] private CharacterControls movementScript;
    [SerializeField] private AudioSource pangSource;
    [SerializeField] private AudioClip pang;

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
        RaycastHit hit;

        currentBulletSpread = Mathf.Min(currentBulletSpread + bulletSpreadTime * Time.deltaTime, bulletSpread);
        float direction = Random.Range(-currentBulletSpread, currentBulletSpread);
        directionVector = transform.forward + transform.right * direction;

        flash.SetActive(true);

        GameObject bullet = Instantiate(
            objectToShoot,
            transform.position,
            Quaternion.LookRotation(directionVector));

        Bullet.CreateComponent(bullet, shootingForce, directionVector, damage, scale, speedOverLifetime: bulletSpeedOverLifetimeCurve_IAmGoodAtNamingThings);

            float currentTime = 0;
            if(currentTime < Time.time)
            {
                pangSource.pitch = Random.Range(0.9f, 1.1f);
                pangSource.PlayOneShot(pang, Random.Range(1.9f, 2.1f));
                currentTime = Time.time + 0.2f;
            }

            if (!Physics.Raycast(movementScript.gameObject.transform.position, new Vector3(Time.deltaTime * knockback * -transform.forward.x, 0, Time.deltaTime * knockback * -transform.forward.z), out hit, 1))
            {
            movementScript.gameObject.transform.position += new Vector3(Time.deltaTime * knockback * -transform.forward.x, 0, Time.deltaTime * knockback * -transform.forward.z);
            }
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private GameObject objectToShoot;
    [SerializeField] private GameObject flash;
    [SerializeField] private CharacterControls movementScript;
    [SerializeField] private AnimationCurve bulletSpeedOverLifetimeCurve_IAmGoodAtNamingThings;
    [SerializeField] private AudioSource pangSource;
    [SerializeField] private AudioClip pang;

    [SerializeField] private float bulletSpeed = 50f;
    [SerializeField] private float fireRate = 0.5f;
    [SerializeField] private float bulletSpread = 0f;
    [SerializeField] private float scale = 1f;
    [SerializeField] private float knockback = 10f;
    [SerializeField] private float bulletSpreadTime = 0f;
    [SerializeField] private float reloadSpeed = 1f;
    [SerializeField] private float bulletLifetime = 3f;

    [SerializeField] private int magazineSize = 64;
    [SerializeField] private int damage;
    [SerializeField] private int bulletAmount = 2;

    private float currentBulletSpread;
    private float timeUntilNextBullet;
    private float currentMag;

    private Vector3 directionVector;

    private void ModifierChange(object sender, (ModifierType modifierType, double value) modifier)
    {
		switch (modifier.modifierType)
		{
			case ModifierType.damage:
				damage = (int)(damage * modifier.value);
				break;
			case ModifierType.movementSpeed:
				break;
			case ModifierType.maxHealth:
				break;
			case ModifierType.healthRegen:
				break;
			case ModifierType.charScale:
				break;
			case ModifierType.magazineSize:
				magazineSize = (int)(magazineSize * modifier.value);
				break;
			case ModifierType.reloadSpeed:
				reloadSpeed = (float)(reloadSpeed * modifier.value);
				break;
			case ModifierType.bulletScale:
				scale = (float)(scale * modifier.value);
				break;
			case ModifierType.bulletLifetime:
				bulletLifetime = (float)(bulletLifetime * modifier.value);
				break;
			case ModifierType.fireRate:
				fireRate = (float)(fireRate / modifier.value);
				break;
			case ModifierType.knockback:
				knockback = (float)(knockback * modifier.value);
				break;
			case ModifierType.bulletSpread:
				bulletSpread = (float)(bulletSpread * modifier.value);
				break;
			case ModifierType.bulletAmount:
				bulletAmount = (bulletAmount + Mathf.RoundToInt((float)modifier.value));
				break;
			case ModifierType.projectileSpeed:
				bulletSpeed = (float)(bulletSpeed * modifier.value);
				break;
			default:
				break;
		}
	}

	private void Start()
    {
        directionVector = transform.forward;
        currentMag = magazineSize;
    }

    private IEnumerator FireBullets()
    {
        int bulletsToFire = bulletAmount;

        while(bulletsToFire > 0)
        {
			flash.SetActive(true);

                currentBulletSpread = Mathf.Min(currentBulletSpread + bulletSpreadTime * Time.deltaTime, bulletSpread);
                float direction = Random.Range(-currentBulletSpread, currentBulletSpread);
                directionVector = transform.forward + transform.right * direction;

                GameObject bullet = Instantiate(
                objectToShoot,
                transform.position,
                Quaternion.LookRotation(directionVector));

                //TODO: add bullet amount
                Bullet.CreateComponent(bullet, bulletSpeed, directionVector, damage, scale, parent: gameObject, destroyDelay: bulletLifetime, speedOverLifetime: bulletSpeedOverLifetimeCurve_IAmGoodAtNamingThings);

            bulletsToFire--;

            float currentTime = 0;
            if (currentTime < Time.time)
            {
                pangSource.pitch = Random.Range(0.9f, 1.1f);
                pangSource.PlayOneShot(pang, Random.Range(1.9f, 2.1f));
                currentTime = Time.time + 0.2f;
            }

            if (!Physics.Raycast(movementScript.gameObject.transform.position, new Vector3(Time.deltaTime * knockback * -transform.forward.x, 0, Time.deltaTime * knockback * -transform.forward.z), out RaycastHit hit, 1))
            {
                movementScript.gameObject.transform.position += new Vector3(Time.deltaTime * knockback * -transform.forward.x, 0, Time.deltaTime * knockback * -transform.forward.z);
            }
        }

        yield return new WaitForSeconds(Random.Range(0.05f, 0.15f));
    }

    private void DoTheShoot()
    {
		currentMag--;

        StartCoroutine(FireBullets());

		/*currentMag--;

        currentBulletSpread = Mathf.Min(currentBulletSpread + bulletSpreadTime * Time.deltaTime, bulletSpread);
        float direction = Random.Range(-currentBulletSpread, currentBulletSpread);
        directionVector = transform.forward + transform.right * direction;

        flash.SetActive(true);

        GameObject bullet = Instantiate(
            objectToShoot,
            transform.position,
            Quaternion.LookRotation(directionVector));

        //TODO: add bullet amount
        Bullet.CreateComponent(bullet, bulletSpeed, directionVector, damage, scale, parent: gameObject, destroyDelay: bulletLifetime, speedOverLifetime: bulletSpeedOverLifetimeCurve_IAmGoodAtNamingThings);

            float currentTime = 0;
            if(currentTime < Time.time)
            {
                pangSource.pitch = Random.Range(0.9f, 1.1f);
                pangSource.PlayOneShot(pang, Random.Range(1.9f, 2.1f));
                currentTime = Time.time + 0.2f;
            }

            if (!Physics.Raycast(movementScript.gameObject.transform.position, new Vector3(Time.deltaTime * knockback * -transform.forward.x, 0, Time.deltaTime * knockback * -transform.forward.z), out RaycastHit hit, 1))
            {
            movementScript.gameObject.transform.position += new Vector3(Time.deltaTime * knockback * -transform.forward.x, 0, Time.deltaTime * knockback * -transform.forward.z);
            }*/
	}

    private void Reload()
    {
        currentMag = magazineSize;
        timeUntilNextBullet = (Time.time + ((0.2f * 10f) / reloadSpeed));
    }

    private IEnumerator SubToModifierChange()
    {
        PlayerShit playerShit = null;

        while (playerShit == null)
        {
            playerShit = PlayerShit.Instance;
            yield return null;
		}

        playerShit.ModifierChange += ModifierChange;
	}

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0) 
            && Time.time > timeUntilNextBullet 
            && movementScript.allowedToMove 
            && currentMag >= 0)
        {
            timeUntilNextBullet = Time.time + fireRate;
            DoTheShoot();
        }
        else if (currentMag <= 0 || Input.GetKey(KeyCode.R))
        {
            Reload();
        }
        else
        {
            currentBulletSpread = Mathf.Max(currentBulletSpread - bulletSpreadTime * 2 * Time.deltaTime, 0);
        }
    }

	private void OnEnable()
	{
		StartCoroutine(SubToModifierChange());
	}

	private void OnDisable()
	{
		PlayerShit.Instance.ModifierChange -= ModifierChange;
	}
}

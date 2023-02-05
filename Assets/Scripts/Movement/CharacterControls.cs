using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterControls : MonoBehaviour
{
    #region Singleton
    [HideInInspector] public static CharacterControls Instance;

	private void OnEnable()
	{
		Instance = this;
		PlayerShit.Instance.ModifierChange += ModifierChange;
	}
    #endregion

    public float speed;

    [SerializeField] private Camera cam;

    public bool allowedToMove = true;
    [SerializeField] private AnimationCurve dashSpeed;
    private float currentTime;
    private Vector3 dashDirection;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashStrenght;
    [SerializeField] private ParticleSystem dashParticleEffect;
    PlayerActor playerActor = null;

	private void ModifierChange(object sender, (ModifierType modifierType, double value) modifier)
	{
        if(playerActor == null)
		{
			playerActor = gameObject.GetComponent<PlayerActor>();
		}

		switch (modifier.modifierType)
		{
			case ModifierType.damage:
				break;
			case ModifierType.movementSpeed:
				speed = (float)(speed * modifier.value);
				break;
			case ModifierType.maxHealth:
                if (playerActor == null) { break; }
				playerActor.MaxHealth = (int)(playerActor.MaxHealth * modifier.value);
				break;
			case ModifierType.healthRegen:
				if (playerActor == null) { break; }
				playerActor.healthRegAsterSeconds = (float)(playerActor.healthRegAsterSeconds / modifier.value);
				break;
			case ModifierType.charScale:
                gameObject.transform.localScale *= (float)modifier.value;
				break;
			case ModifierType.magazineSize:
				break;
			case ModifierType.reloadSpeed:
				break;
			case ModifierType.bulletScale:
                break;
			case ModifierType.bulletLifetime:
				break;
			case ModifierType.fireRate:
				break;
			case ModifierType.knockback:
				break;
			case ModifierType.bulletSpread:
				break;
			case ModifierType.bulletAmount:
				break;
			case ModifierType.projectileSpeed:
				break;
			default:
				break;
		}
	}

	private void PleaseMove()
    {
        RaycastHit hit;

        if (allowedToMove)
        {
            if (Input.GetKey(KeyCode.W))
            {
                if (!Physics.Raycast(transform.position, Time.deltaTime * speed * new Vector3(0, 0, 1), out hit, 1))
                {

                    gameObject.transform.position +=
                    Time.deltaTime * speed * new Vector3(0, 0, 1);
                }
            }
            if (Input.GetKey(KeyCode.S))
            {
                if (!Physics.Raycast(transform.position, Time.deltaTime * -speed * new Vector3(0, 0, 1), out hit, 1))
                {

                    gameObject.transform.position +=
                    Time.deltaTime * -speed * new Vector3(0, 0, 1);
                }
            }
            if (Input.GetKey(KeyCode.D))
            {
                if (!Physics.Raycast(transform.position, Time.deltaTime * speed * new Vector3(1, 0, 0), out hit, 1))
                {

                    gameObject.transform.position +=
                    Time.deltaTime * speed * new Vector3(1, 0, 0);
                }
            }
            if (Input.GetKey(KeyCode.A))
            {
                if (!Physics.Raycast(transform.position, Time.deltaTime * -speed * new Vector3(1, 0, 0), out hit, 1))
                {
                    gameObject.transform.position +=
                    Time.deltaTime * -speed * new Vector3(1, 0, 0);
                }
            }
        }
    }

    private void LookAtMouseCyka()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Plane plane = new(Vector3.up, transform.position);


        if (plane.Raycast(ray, out float distance))
        {
            Vector3 lookPoint = ray.GetPoint(distance);
            transform.rotation = Quaternion.LookRotation(lookPoint - transform.position);
        }
    }


    private void Dash()
    {
        if (allowedToMove == true)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                allowedToMove = false;
                currentTime = Time.time;
                dashParticleEffect.Play();

                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
                {
                    dashDirection = new Vector3(0, 0, 0);
                }

                if (Input.GetKey(KeyCode.W))
                {
                    dashDirection.z = 1f;
                }
                if (Input.GetKey(KeyCode.S))
                {
                    dashDirection.z = -1f;
                }
                if (Input.GetKey(KeyCode.D))
                {
                    dashDirection.x = 1f;
                }
                if (Input.GetKey(KeyCode.A))
                {
                    dashDirection.x = -1f;
                }

                if (dashDirection.x != 0 && dashDirection.z != 0)
                {
                    dashDirection *= 0.75f;
                }
            }
        }
    }

    private void Dashing()
    {
        RaycastHit hit;

        if (allowedToMove == false)
        {
            if (!Physics.Raycast(transform.position, Time.deltaTime * dashSpeed.Evaluate(Time.time - currentTime) * speed * dashDirection * dashStrenght, out hit, 1))
            {
                gameObject.transform.position += Time.deltaTime * dashSpeed.Evaluate(Time.time - currentTime) * speed * dashDirection * dashStrenght;
            }

            if (Time.time > currentTime + dashDuration)
            {
                allowedToMove = true;
            }
        }
    }

    private void Update()
    {
        PleaseMove();
        Dash();
        Dashing();
    }

    private void FixedUpdate()
    {
        LookAtMouseCyka();
    }

	private void OnDisable()
	{
		PlayerShit.Instance.ModifierChange -= ModifierChange;
	}
}

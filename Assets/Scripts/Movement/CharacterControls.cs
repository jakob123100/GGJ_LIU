using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControls : MonoBehaviour
{
    #region Singleton
    [HideInInspector] public static CharacterControls Instance;

	private void OnEnable()
	{
		Instance = this;
	}
    #endregion

    public float velocity;

    [SerializeField] private Camera cam;

    public bool allowedToMove = true;
    [SerializeField] private AnimationCurve dashSpeed;
    private float currentTime;
    private Vector3 dashDirection;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashStrenght;
    [SerializeField] private ParticleSystem dashParticleEffect;

    private void PleaseMove()
    {
        RaycastHit hit;

        if (allowedToMove)
        {
            if (Input.GetKey(KeyCode.W))
            {
                if (!Physics.Raycast(transform.position, Time.deltaTime * velocity * new Vector3(0, 0, 1), out hit, 1))
                {

                    gameObject.transform.position +=
                    Time.deltaTime * velocity * new Vector3(0, 0, 1);
                }
            }
            if (Input.GetKey(KeyCode.S))
            {
                if (!Physics.Raycast(transform.position, Time.deltaTime * -velocity * new Vector3(0, 0, 1), out hit, 1))
                {

                    gameObject.transform.position +=
                    Time.deltaTime * -velocity * new Vector3(0, 0, 1);
                }
            }
            if (Input.GetKey(KeyCode.D))
            {
                if (!Physics.Raycast(transform.position, Time.deltaTime * velocity * new Vector3(1, 0, 0), out hit, 1))
                {

                    gameObject.transform.position +=
                    Time.deltaTime * velocity * new Vector3(1, 0, 0);
                }
            }
            if (Input.GetKey(KeyCode.A))
            {
                if (!Physics.Raycast(transform.position, Time.deltaTime * -velocity * new Vector3(1, 0, 0), out hit, 1))
                {
                    gameObject.transform.position +=
                    Time.deltaTime * -velocity * new Vector3(1, 0, 0);
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

    // Update is called once per frame
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
            if (!Physics.Raycast(transform.position, Time.deltaTime * dashSpeed.Evaluate(Time.time - currentTime) * velocity * dashDirection * dashStrenght, out hit, 1))
            {
                gameObject.transform.position += Time.deltaTime * dashSpeed.Evaluate(Time.time - currentTime) * velocity * dashDirection * dashStrenght;
            }

            if (Time.time > currentTime + dashDuration)
            {
                allowedToMove = true;
            }
        }
    }

}

using UnityEngine;

public class CharacterControls : MonoBehaviour
{
    #region Singleton
    public static CharacterControls Instance;

	private void OnEnable()
	{
		Instance = this;
	}
	#endregion

	[SerializeField] private float velocity = 10.0f;
    [SerializeField] private Camera cam;
    [SerializeField] private Rigidbody body;

    private void PleaseMove()
    {
        if (Input.GetKey(KeyCode.W))
        {
            gameObject.transform.position +=
                Time.deltaTime * velocity * new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            gameObject.transform.position +=
                Time.deltaTime * -velocity * new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.D))
        {
            gameObject.transform.position +=
                Time.deltaTime * velocity * new Vector3(1, 0, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            gameObject.transform.position +=
                Time.deltaTime * -velocity * new Vector3(1, 0, 0);
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
    }

    private void FixedUpdate()
    {
        LookAtMouseCyka();
    }

}

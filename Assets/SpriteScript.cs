using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteScript : MonoBehaviour
{
    void Update()
    {
		Camera mainCamera = Camera.main;
		Vector3 cameraDirection = Vector3.Normalize(mainCamera.transform.position - transform.position);
		Quaternion cameraRotation = Quaternion.LookRotation(cameraDirection);
		cameraRotation.x = 0;
		cameraRotation.y = 0;
		transform.rotation = cameraRotation;
	}
}

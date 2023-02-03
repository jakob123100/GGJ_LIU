using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothSpeed = 0.125f;
    [SerializeField] private float cameraHeight = 25f;
    [SerializeField] private float cameraZOffset = -5f;
    [SerializeField] private float cameraXOffset = 0f;

    private void LateUpdate()
    {
        Vector3 desiredPosition =
            new Vector3(
                target.position.x + cameraXOffset,
                target.position.y + cameraHeight,
                target.position.z + cameraZOffset);

        Vector3 smoothedPosition =
            Vector3.Lerp(
                transform.position,
                desiredPosition,
                smoothSpeed);

        transform.SetPositionAndRotation(
            smoothedPosition,
            Quaternion.Euler(75f, 0f, 0f));
    }
}

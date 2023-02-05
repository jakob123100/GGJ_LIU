using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    private PlayerActor playerActor;

    private void Start()
    {
        playerActor = gameObject.GetComponent<PlayerActor>();
    }
    void Update()
    {
        transform.LookAt(playerActor.transform, Vector3.up);
    }
}

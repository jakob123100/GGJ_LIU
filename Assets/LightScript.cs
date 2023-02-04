using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour
{
    private float time = 0;

    private void OnEnable()
    {
        time = Time.time + 0.02f;
    }

    private void Update()
    {
        if(time < Time.time && time != 0)
        {
            time = 0;
            gameObject.SetActive(false);
        }
    }

}

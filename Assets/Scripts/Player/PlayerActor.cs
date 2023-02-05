using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerActor : Actor
{
	public float healthRegAsterSeconds = 10f;

	private float lastHealthRegTime = 0f;

	private void Update()
	{
		if(Time.time - lastHealthRegTime > healthRegAsterSeconds)
		{
			lastHealthRegTime = Time.time;

			if(health < MaxHealth)
			{
				health++;
			}
			else
			{
				health = MaxHealth;
			}
		}

		if (mat != null)
		{
			if (Time.time > currentTime)
			{
				mat.GetComponent<Renderer>().material.color = Color.white;
			}
		}
	}
}

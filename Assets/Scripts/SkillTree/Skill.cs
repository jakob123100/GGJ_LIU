using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
	[SerializeField] ModifierType buffModifier;
	[SerializeField] double buffValue;
	[SerializeField] ModifierType debuffModifier;
	[SerializeField] double debuffValue;

    public void ApplyModifiers()
    {
		PlayerShit.Instance.AddModifier(buffModifier, buffValue);
		PlayerShit.Instance.AddModifier(debuffModifier, debuffValue);
    }
}

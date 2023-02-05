using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameMenu : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI killCountText;

	private void OnEnable()
	{
		killCountText.text = $"Kill Count: {GameController.Instance.killCount}";
	}
}

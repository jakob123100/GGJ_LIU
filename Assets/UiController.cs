using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI killCountText;

    int killCount = 0;

	private void Start()
	{
		killCount = GameController.Instance.killCount;
		killCountText.text = $"Kill Count: {GameController.Instance.killCount}";
	}

	// Update is called once per frame
	void Update()
    {
        if(GameController.Instance.killCount != killCount)
        {
            killCount = GameController.Instance.killCount;
			killCountText.text = $"Kill Count: {GameController.Instance.killCount}";
		}
    }
}

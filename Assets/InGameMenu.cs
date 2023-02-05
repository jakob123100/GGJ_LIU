using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
	public void Continue()
	{
		gameObject.SetActive(false);

		Time.timeScale = 1f;
	}

	public void Restart()
	{
		SceneManager.LoadScene("I forgor");
	}

	public void GoToMainMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}

	public void Quit()
	{
		Application.Quit();
	}
}

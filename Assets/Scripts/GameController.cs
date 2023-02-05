using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
	[SerializeField] private GameObject SkillTree;
    [SerializeField] private int killsNeededForLevel = 10;
	[SerializeField] private GameObject InGameMenu;

    public int killCount = 0;

	#region Singleton
	[HideInInspector] public static GameController Instance;

	private void OnEnable()
	{
		Instance = this;
	}
	#endregion

	private void OnLevelupDone(object sender, EventArgs e)
	{
		SkillTree.SetActive(false);
		Time.timeScale = 1;
		SkillTreeGenerator.Instance.PickedSkill -= OnLevelupDone;
	}

	private void LevelUp()
	{
		Time.timeScale = 0;
		SkillTree.SetActive(true);
		SkillTreeGenerator.Instance.PickSkill();
		SkillTreeGenerator.Instance.PickedSkill += OnLevelupDone;
	}

	public void EnemyKilled()
	{
		killCount++;
		if(killCount % killsNeededForLevel == 0)
		{
			LevelUp();
		}
	}

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			InGameMenu.SetActive(!InGameMenu.active);

			Time.timeScale = InGameMenu.active ? 0 : 1;
		}
	}
}

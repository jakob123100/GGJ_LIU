using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkillNode
{
	public Skill skillScript;

	public SkillNode(Skill skillScript)
    {
        this.skillScript = skillScript;
    }
}

public class SkillTreeGenerator : MonoBehaviour
{
    [SerializeField] private Vector2 nodeOffset;
	[SerializeField] private int TreeStartHeight = 2;
	[SerializeField] private int TreeHeight = 0;
	[SerializeField] private GameObject[] skillPrefabs;

	SkillNode[] skillTree;

	private GameObject GetRandomSkillObject()
    {
        int randomIndex = Random.Range(0, skillPrefabs.Length);
        return skillPrefabs[randomIndex];
    }

	private void GenerateLevel()
	{
		TreeHeight++;
		SkillNode[] newTree = new SkillNode[skillTree.Length + TreeHeight];

		for (int i = 0; i < skillTree.Length; i++)
		{
			newTree[i] = skillTree[i];
		}

		for (int i = skillTree.Length; i < skillTree.Length + TreeHeight; i++)
		{
			GameObject skillObject = Instantiate(GetRandomSkillObject(), gameObject.transform);
			Skill skill = skillObject.GetComponent<Skill>();
			newTree[i] = new SkillNode(skill);
		}

		skillTree = newTree;

		UpdateSkillObjectsPos();
	}

	private void TraverseLeft()
	{
		SkillNode[] newTree = new SkillNode[skillTree.Length - TreeHeight];

		int i = 0;
		int j = 0;

		for (int y = 0; y < TreeHeight; y++)
		{
			for (int x = 0; x < y; x++)
			{
				newTree[i] = skillTree[j];

				i++;
				j++;
			}
			Destroy(skillTree[j].skillScript.gameObject);
			j++;
		}

		TreeHeight--;

		skillTree = newTree;

		GenerateLevel();

		UpdateSkillObjectsPos();
	}

	private void TraverseRight()
	{
		SkillNode[] newTree = new SkillNode[skillTree.Length - TreeHeight];

		int i = 0;
		int j = 0;

		for (int y = 0; y < TreeHeight; y++)
		{
			Destroy(skillTree[j].skillScript.gameObject);
			j++;
			for (int x = 1; x <= y; x++)
			{
				newTree[i] = skillTree[j];

				i++;
				j++;
			}
		}

		TreeHeight--;

		skillTree = newTree;

		GenerateLevel();

		UpdateSkillObjectsPos();
	}

	private void UpdateSkillObjectsPos()
	{
		int i = 0;

		for (int y = 0; y < TreeHeight; y++)
		{
			for (int x = 0; x <= y; x++)
			{
				RectTransform rectTransform = skillTree[i].skillScript.gameObject.GetComponent<RectTransform>();

				if (rectTransform == null)
				{
					continue;
				}

				rectTransform.anchoredPosition = new Vector3((x - y/2f) * nodeOffset.x, -y * nodeOffset.y, 0);

				i++;
			}
		}
	}

	// Start is called before the first frame update
	void Start()
    {
        skillTree = new SkillNode[0];

        TreeHeight = 0;

		for (int i = 0; i < TreeStartHeight; i++)
        {
            GenerateLevel();
        }
    }

	public bool left = false;
	public bool right = false;
	public bool generateNextLayer = false;
	// Update is called once per frame
	void Update()
    {
		if (left)
		{
			left = false;
			skillTree[1].skillScript.ApplyModifiers();
			TraverseLeft();
		}
		if (right)
		{
			right = false;
			skillTree[2].skillScript.ApplyModifiers();
			TraverseRight();
		}
		if (generateNextLayer)
		{
			generateNextLayer = false;
			GenerateLevel();
		}

	}
}

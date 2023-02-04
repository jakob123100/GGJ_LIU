using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ModifyerType
{
    damage,
    movementSpeed,
    maxHealth,
    healthRegen,
    charScale
}

public class PlayerShit : MonoBehaviour
{
    #region Singleton
    public static PlayerShit Instance;

	private void OnEnable()
	{
		Instance= this;
	}
    #endregion

    public static event EventHandler<(ModifyerType, double)> ModifierChange;

	[SerializeField] private float baseMoveSpeed = 100f;
    [SerializeField] private float baseMaxHealth = 1000;
    [SerializeField] private float baseHealthRegen = 100;
    [SerializeField] private float baseDodgeRate = 1000;
    [SerializeField] private float baseArmor = 1000;
    [SerializeField] private float baseCharSize = 1000;

    private float currentMoveSpeed;
    private float currentMaxHealth;
    private float currentHealthRegen;
    private float currentDodgeRate;
    private float currentArmor;
    private float currentCharSize;

    List<(ModifyerType, double)> modifiers;

    public double GetModifier(ModifyerType modifyerType)
    {
        double value = 1f;

        foreach ((ModifyerType, double) modifier in modifiers)
        {
            if(modifier.Item1 == modifyerType)
            {
                value *= modifier.Item2;
            }
        }

        return value;
    }

	public void AddModifier(ModifyerType modifyerType, double value)
    {
        (ModifyerType, double) tupple = (modifyerType, value);
        modifiers.Add(tupple);

		OnModifierChange(modifyerType, value);
    }

    private void OnModifierChange(ModifyerType modifyerType, double value)
    {
		(ModifyerType, double) change = (modifyerType, value);
        ModifierChange?.Invoke(this, change);
	}

    void Start()
    {
        currentMoveSpeed    = baseMoveSpeed;
        currentMaxHealth    = baseMaxHealth;
        currentHealthRegen  = baseHealthRegen;
        currentDodgeRate    = baseDodgeRate;
        currentArmor        = baseArmor;
        currentCharSize     = baseCharSize;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnDisable()
	{
		
	}
}

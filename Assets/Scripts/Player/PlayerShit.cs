using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ModifierType
{
    damage,
    movementSpeed,
    maxHealth,
    healthRegen,
    charScale,
    magazineSize,
    reloadSpeed
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

    public static event EventHandler<(ModifierType, double)> ModifierChange;

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

    List<(ModifierType, double)> modifiers;

    public double GetModifier(ModifierType modifyerType)
    {
        double value = 1f;

        foreach ((ModifierType, double) modifier in modifiers)
        {
            if(modifier.Item1 == modifyerType)
            {
                value *= modifier.Item2;
            }
        }

        return value;
    }

	public void AddModifier(ModifierType modifyerType, double value)
	{
		(ModifierType, double) tupple = (modifyerType, value);
		modifiers.Add(tupple);

		OnModifierChange(modifyerType, value);
	}

	public void AddModifier((ModifierType, double) modifier)
	{
		modifiers.Add(modifier);

		OnModifierChange(modifier.Item1, modifier.Item2);
	}

	private void OnModifierChange(ModifierType modifyerType, double value)
    {
		(ModifierType, double) change = (modifyerType, value);
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

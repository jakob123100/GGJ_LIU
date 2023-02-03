using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShit : MonoBehaviour
{
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
}

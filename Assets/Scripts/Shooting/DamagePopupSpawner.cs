using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class DamagePopupSpawner : MonoBehaviour
{
    [SerializeField] private float floatingUpSpeed = 4f;
    [SerializeField] private float floatingSideSpeed = 4f;

    private float randomSideSpeed;
    private TextMeshPro textMesh;

    public static DamagePopupSpawner Create(Vector3 position, int damage)
    {
        Transform popupPrefab = 
            AssetDatabase.LoadAssetAtPath<Transform>("Assets/pREfABS/DamagePopup.prefab");

        Transform damagePopupTransform = Instantiate(
            popupPrefab,
            position,
            Quaternion.identity);

        DamagePopupSpawner damagePopup = damagePopupTransform
            .GetComponent<DamagePopupSpawner>();

        damagePopup.SetDamage(damage);

        return damagePopup;
    }

    private void FloatUp()
    {
        transform.position += new Vector3(
            0, 
            floatingUpSpeed, 
            randomSideSpeed) * Time.fixedDeltaTime;
    }

    private void SetDamage(int damage)
    {
        textMesh.SetText(damage.ToString());
    }

    private void Update()
    {
        FloatUp();
    }

    private void Awake()
    {
        randomSideSpeed = Random.Range(
            -floatingSideSpeed,
            floatingSideSpeed);
        textMesh = transform.GetComponent<TextMeshPro>();
    }
}

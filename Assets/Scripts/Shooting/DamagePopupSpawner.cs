using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class DamagePopupSpawner : MonoBehaviour
{
    [SerializeField] private float floatingUpSpeed = 4f;
    [SerializeField] private float floatingSideSpeed = 4f;
    [SerializeField] private float dissapearSpeed = 2f;
    [SerializeField] private float maxDeathTimer = 1f;
    [SerializeField] private float inflateSpeed = 1f;
 

    private float deathTimer;
    private float randomSideSpeed;
    private TextMeshPro textMesh;
    private Color textColor;

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
            randomSideSpeed, 
            floatingUpSpeed,
            0) * Time.fixedDeltaTime;
    }

    private void SetDamage(int damage)
    {
        textMesh.SetText(damage.ToString());
    }

    private void DeathFade()
    {
        textColor.a -= dissapearSpeed * Time.fixedDeltaTime;
        textMesh.color = textColor;

        if(textColor.a <= 0 )
        {
            Destroy(gameObject);
        }
    }

    private void ScaleChange()
    {
        //Inflates
        if(deathTimer > maxDeathTimer / 2)
        {
            transform.localScale += 
                inflateSpeed * Time.fixedDeltaTime * Vector3.one;
        }
        //Deflates
        else
        {
            transform.localScale -=
                inflateSpeed * Time.fixedDeltaTime * Vector3.one;
        }
    }

    private void FixedUpdate()
    {
        FloatUp();
        ScaleChange();

        deathTimer -= Time.fixedDeltaTime;
        if(deathTimer < 0)
        {
            DeathFade();
        }
    }

    private void Awake()
    {
        deathTimer = maxDeathTimer;

        randomSideSpeed = Random.Range(
            -floatingSideSpeed,
            floatingSideSpeed);
        textMesh = transform.GetComponent<TextMeshPro>();

        textColor = textMesh.color;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class SpawningManager : MonoBehaviour
{
    private List<GameObject> allEnemies = new();

    [SerializeField] private GameObject enemyOne;
    [SerializeField] private GameObject enemyTwo;
    [SerializeField] private GameObject enemyThree;
    [SerializeField] private float spawnRate = 5f;

    [SerializeField] private float spawnRadius = 10f;   // Set the spawn radius here
    private readonly float buffer = 10f;
    private float timeToNextSpawn;

    private GameObject PickRandomEnemy()
    {
        int randomNumber = Random.Range(
            0,
            allEnemies.Count);

        return allEnemies[randomNumber];
    }

    private void Spawn()
    {
        //Viewport bounts
        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, transform.position.y, 0));
        Vector3 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, transform.position.y, 1));

        Vector3 spawnPosition = transform.position + new Vector3(
            Random.Range(bottomLeft.x - buffer, topRight.x + buffer),
            0,
            Random.Range(bottomLeft.z - buffer, topRight.z + buffer)
        );

        Instantiate(PickRandomEnemy(), spawnPosition, Quaternion.identity);
    }

    private void Update()
    {
       if (Time.time > timeToNextSpawn)
        {
            float nextSpawnRandom = Random.Range(spawnRate / 2, spawnRate * 2);
            timeToNextSpawn = Time.time + nextSpawnRandom;
            Spawn();
        }
    }

    private void Start()
    {
        allEnemies.Add(enemyOne);
        allEnemies.Add(enemyTwo);
        allEnemies.Add(enemyThree);
    }
}

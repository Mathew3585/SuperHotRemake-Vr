using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] enemies;
    public Transform[] spawnPoints;
    public int maxEnemies;
    [HideInInspector]
    public int currentEnemyLife;

    // Start is called before the first frame update
    void Start()
    {
        currentEnemyLife = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentEnemyLife < maxEnemies)
        {
            int enemiesToSpawn = maxEnemies - currentEnemyLife;
            for (int i = 0; i < enemiesToSpawn; i++)
            {
                if (currentEnemyLife >= maxEnemies)
                    break;

                Transform currentSpawn = spawnPoints[Random.Range(0, spawnPoints.Length)];
                Instantiate(enemies[Random.Range(0, enemies.Length)], currentSpawn.position, currentSpawn.rotation);
                currentEnemyLife++;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerUpPrefab;
    public float posRange = 9.0f;
    private int enemiesAlive;
    private int level = 1;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWaves(level);
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        enemiesAlive = FindObjectsOfType<Enemy>().Length;

        if (enemiesAlive == 0 && gameManager.gameIsActive)
        {
            level++;
            SpawnEnemyWaves(level);
        }
    }

    private void SpawnEnemyWaves(int enemiesToSpawn)
    {
        Instantiate(powerUpPrefab.gameObject, GenRandomPos(), powerUpPrefab.transform.rotation);

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenRandomPos(), enemyPrefab.transform.rotation);
        }
    }

    private Vector3 GenRandomPos()
    {
        float enemyPosX = Random.Range(-posRange, posRange);
        float enemyPosZ = Random.Range(-posRange, posRange);

        Vector3 enemyPos = new Vector3(enemyPosX, 0, enemyPosZ);

        return enemyPos;
    }
}

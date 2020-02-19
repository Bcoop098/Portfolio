using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Make sure there is only one enemy spawner.
    private static EnemySpawner instance = null;
    
    // Enemy prefab.
    [SerializeField]
    private GameObject decrepit;

    [SerializeField]
    private GameObject hollow;

    [SerializeField]
    private GameObject rupture;


    // Spawn rate in seconds.
    [SerializeField]
    private int decrepitSpawnRate = 3;

    [SerializeField]
    private int hollowSpawnRate = 5;

    [SerializeField]
    private int ruptureSpawnRate = 7;

    // Delay between wave spawns.
    [SerializeField]
    private int waveDelay = 10;
    [SerializeField]
    private int currentSpawn = 0;

    private int currentWave = 1;

    private int maxWaves = 0;

    private int numDecrepit = 3;
    private int numHollow = 2;
    private int numRupture = 1;

    // Keep track of the enemies that are spawned.
    private List<GameObject> spawnedEnemies;

    int enemyKillCount = 0;


    // Enemy count for each wave.
    [SerializeField]
    private List<int> spawnCount;

    private bool canSpawn = true;

    private GameManager gameManager;

    private bool startNextWave = false;

    private TextMeshProUGUI waveCount;
    
    private TextMeshProUGUI remainingSpawns;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        gameManager = GameManager.instance;

        // Set the max spawn count to the max number of spawns created.
        maxWaves = spawnCount.Count;

        gameManager.SetMaxWaves(maxWaves);

        spawnedEnemies = new List<GameObject>();

        StartCoroutine("SpawnDecrepit");

        waveCount = GameObject.Find("WaveCount_TMP").GetComponent<TextMeshProUGUI>();
        remainingSpawns = GameObject.Find("RemainingEnemyCount_TMP").GetComponent<TextMeshProUGUI>();

        waveCount.text = currentWave.ToString();
        remainingSpawns.text = (spawnCount[0]).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForEnemies();
        UpdateSpawnedEnemyList();
    }

    private IEnumerator SpawnDecrepit()
    {

        if (canSpawn)
        {
            for (int i = 0; i < (numDecrepit * currentWave); i++)
            {
                currentSpawn++;
                spawnedEnemies.Add(Instantiate(decrepit, transform));
                yield return new WaitForSeconds(decrepitSpawnRate);
            }
        }
    }

    private IEnumerator SpawnHollow()
    {
        if (canSpawn)
        {
            for (int i = 0; i < (numHollow * (currentWave-1)); i++)
            {
                currentSpawn++;
                spawnedEnemies.Add(Instantiate(hollow, transform));
                yield return new WaitForSeconds(hollowSpawnRate);
            }
        }
    }

    private IEnumerator SpawnRupture()
    {
        if (canSpawn)
        {
            for (int i = 0; i < (numRupture * (currentWave - 5)); i++)
            {
                currentSpawn++;
                spawnedEnemies.Add(Instantiate(rupture, transform));
                yield return new WaitForSeconds(ruptureSpawnRate);
            }
        }
    }

    private void CheckForEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // There are enemies and the last enemy has been spawned.
        if (enemies.Length != 0 && spawnCount[gameManager.GetCurrentWave() - 1] == currentSpawn)
        {
            canSpawn = false;
        }
        // There are no enemies and the wave is done.
        else if (enemies.Length == 0 && !canSpawn && !startNextWave)
        {
            startNextWave = true;
            StartCoroutine("SwitchWaves");
        }
    }

    private IEnumerator SwitchWaves()
    {
        gameManager.UpdateWave();

        yield return new WaitForSeconds(waveDelay);

        currentWave = gameManager.GetCurrentWave();
        startNextWave = false;
        canSpawn = true;
        currentSpawn = 0;
        enemyKillCount = 0;

        waveCount.text = currentWave.ToString();
        remainingSpawns.text = (spawnCount[currentWave - 1] - enemyKillCount).ToString();

        if (currentWave >= 2)
        {
            StartCoroutine("SpawnDecrepit");
            StartCoroutine("SpawnHollow");
            if (currentWave >= 6)
            {
                StartCoroutine("SpawnRupture");
            }
        }

    }

    private void UpdateSpawnedEnemyList()
    {
        if (spawnedEnemies.Count != 0)
        {
            GameObject lastEnemy = spawnedEnemies[spawnedEnemies.Count - 1];

            int enemyCounter = 0;

            while (spawnedEnemies[enemyCounter] != lastEnemy)
            {
                if (spawnedEnemies[enemyCounter] == null)
                {
                    spawnedEnemies.RemoveAt(enemyCounter);
                    enemyKillCount++;
                    remainingSpawns.text = (spawnCount[currentWave - 1] - enemyKillCount).ToString();
                }
                else
                {
                    enemyCounter++;
                }
            }

            if (lastEnemy == null)
            {
                spawnedEnemies.Clear();
                enemyKillCount++;
                remainingSpawns.text = (spawnCount[currentWave - 1] - enemyKillCount).ToString();
            }
        }
    }
}

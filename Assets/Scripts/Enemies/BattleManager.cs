using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{

    public static BattleManager instance;

    [SerializeField] MobSpawnPosList spawnPosListScriptable;
    private List<GameObject> spawnedPositions = new List<GameObject>();
    [SerializeField] List<GameObject> zombieList;
    [SerializeField] SavedGame savedGameScore;
    [SerializeField] int amountToSpawnByWave = 5;
    private int spawned = 0;
    private int totalSpawned = 0;
    public int zombieKilledInTotal;
    [SerializeField] int timeBetweenSpawns = 1;
    private int zombieKilledInWave;
    private bool inWave = false;
    private bool gameStarted = false;
    private int numberOfWaves = 0;

    [Header("UI:")]
    [SerializeField] GameObject waveInfo;
    [SerializeField] GameObject scoreText;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Creates the SpawnPoints 
        for (var i = 0; i < spawnPosListScriptable.spawnPosList.Length; i++)
        {
            GameObject obj = Instantiate(spawnPosListScriptable.spawnPosList[i]);
            spawnedPositions.Add(obj);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!inWave && !gameStarted)
        {
            StartCoroutine(WaitToStartNewWave());
            gameStarted = true;
        }
        if (zombieKilledInWave == (amountToSpawnByWave + numberOfWaves) && inWave == true)
        {
            inWave = false;
            StartCoroutine(WaitToStartNewWave());
        }
        StartCoroutine(SpawnWithDelay());
    }

    // Counts the amount of zombies killed and decreases amount of spawned/alive 
    public void ZombieKilled()
    {
        spawned--;
        zombieKilledInWave++;
        zombieKilledInTotal++;
        scoreText.GetComponent<Text>().text = zombieKilledInTotal.ToString();
        if (zombieKilledInTotal > savedGameScore.GetHighScore()) savedGameScore.SetHighScore(zombieKilledInTotal);
    }

    // Resets wave  
    private void StartNewWave()
    {
        spawned = 0;
        totalSpawned = 0;
        zombieKilledInWave = 0;
        inWave = true;
        numberOfWaves++;
    }

    IEnumerator SpawnWithDelay()
    {
        if (inWave && gameStarted)
        {
            if ((spawned < System.Math.Ceiling((amountToSpawnByWave + numberOfWaves) / 3f))
                && totalSpawned < (amountToSpawnByWave + numberOfWaves))
            {
                float spawnChance = Random.Range(0, 100);
                if (spawnChance > 80)
                {
                    // spawn RunnerZombie (20% chance)
                    Instantiate(zombieList[1],
                        spawnedPositions[Random.Range(1, spawnedPositions.Count)].transform);
                }
                else
                {
                    // spawn NormalZombie
                    Instantiate(zombieList[0],
                        spawnedPositions[Random.Range(1, spawnedPositions.Count)].transform);
                }
                spawned++;
                totalSpawned++;
                yield return new WaitForSeconds(timeBetweenSpawns);
            }
        }
    }

    IEnumerator WaitToStartNewWave()
    {
        waveInfo.SetActive(true);
        yield return new WaitForSeconds(3.5f);
        waveInfo.SetActive(false);
        StartNewWave();
    }
}


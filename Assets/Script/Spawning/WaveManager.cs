using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public SpawnPoint[] spawnPoints;
    public float postWavePause = 5f;

    [Header("Read Only")]
    public int currentWave;
    public int levelMaxWaves = 0;
    public int monstersAlive = 0;

    private bool started = false;
    private int currentLevel;
    private bool waveDataLoaded;
    private int loadedCount;
    private bool allWavesComplete;
    private LevelWaveInstructions[] spawnPointLevelWaveInstructions;

    private MonsterManager monsterManager;
    void Start()
    {
        currentLevel = GetComponent<LevelManager>().currentLevel;
        monsterManager = GetComponent<MonsterManager>();

        spawnPointLevelWaveInstructions = new LevelWaveInstructions[spawnPoints.Length];

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            LoadInstructionsFromFilePath(i);
        }
    }

    private void CheckWaveComplete()
    {
        // if there are still monsters left alive we havent finished 
        if (monsterManager.monstersAlive > 0)
        {
            Invoke("CheckWaveComplete", 1f);
            return;
        }

        // if there are no monsters alive but spawn points havent finished spawning then we havent finished
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (!spawnPoints[i].finishedSpawningCurrentWave)
            {
                Invoke("CheckWaveComplete", 1f);
                return;
            }
        }

        // if we're finished with all waves then complete the level
        if (currentWave == levelMaxWaves)
        {
            Debug.Log("LEVEL COMPLETE");
            return;
        }
    
        // if no alive monsters and all spawning is done and there are waves left to go then spawn the next wave
        Invoke("StartNextWave", postWavePause);
        Invoke("CheckWaveComplete", postWavePause + 1f);
    }

    private void StartNextWave()
    {
        StartWave(currentWave + 1);
    }

    private void FixedUpdate()
    {
        if (waveDataLoaded && !started)
        {
            monstersAlive = 0;
            started = true;
            StartWave(0);
            Invoke("CheckWaveComplete", 1f);
        }
    }

    public void StartWave(int waveIndex)
    {
        currentWave = waveIndex;
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (waveIndex < spawnPointLevelWaveInstructions[i].waves.Count) {
                spawnPoints[i].SetInstructions(spawnPointLevelWaveInstructions[i].waves[waveIndex].instructions);
                spawnPoints[i].StartWave();
            }
        }
    }

    private void HandleWaveDataLoaded (LevelWaveInstructions levelWaveInstructions, int spawnPointIndex)
    {
        spawnPointLevelWaveInstructions[spawnPointIndex] = levelWaveInstructions;
        levelMaxWaves = Mathf.Max(levelMaxWaves, levelWaveInstructions.waves.Count);

        loadedCount++;
        if (loadedCount == spawnPoints.Length)
        {
            waveDataLoaded = true;
        }
    }

    public WaveInstructions LoadInstructionsFromFilePath(int spawnPointIndex)
    {
        string fileName = $"wave-instructions\\level-{currentLevel}\\spawn-point-{spawnPointIndex}.json";
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

        StartCoroutine(loadWaveStreamingAsset(filePath, spawnPointIndex));
        return null;
    }

    IEnumerator loadWaveStreamingAsset(string filePath, int spawnPointIndex)
    {
        if (filePath.Contains("://") || filePath.Contains(":///"))
        {
            WWW www = new WWW(filePath);
            yield return www;
            LevelWaveInstructions waveData = JsonUtility.FromJson<LevelWaveInstructions>(www.text);
            HandleWaveDataLoaded(waveData, spawnPointIndex);
        }
        else
        {
            if (File.Exists(filePath))
            {
                // Read the json from the file into a string
                string dataAsJson = File.ReadAllText(filePath);
                // Pass the json to JsonUtility, and tell it to create a GameData object from it
                LevelWaveInstructions waveData = JsonUtility.FromJson<LevelWaveInstructions>(dataAsJson);
                HandleWaveDataLoaded(waveData, spawnPointIndex);
            }
            else
            {
                Debug.LogError($"Cannot load {filePath} data!");
            }
        }
    }
}

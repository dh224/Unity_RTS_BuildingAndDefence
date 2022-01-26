using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyWaveManager : MonoBehaviour
{
    public static EnemyWaveManager Instance { get; private set; }

    // Start is called before the first frame update
    public event EventHandler OnWaveNumberChanged;
    
    private enum State
    {
        WaitingToSpawnNextWave,
        SpawningWave,
    }

    private State state;
    private float nextWaveSpawnTimer;
    private int remainingEnemySpawnAmount;
    private float nextEnemySpawnTimer;
    private int waveNumber = 0;

    [SerializeField] private List<Transform> spawnPositionTransform;
    [SerializeField] private Transform nextWaveSpawnPositionTransform;
    private Vector3 spawnPosition;
    void Start()
    {
        Instance = this;
        state = State.WaitingToSpawnNextWave;
        spawnPosition = spawnPositionTransform[Random.Range(0,spawnPositionTransform.Count -1)].position;
        nextWaveSpawnPositionTransform.position = spawnPosition;
        nextWaveSpawnTimer = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.WaitingToSpawnNextWave:
                nextWaveSpawnTimer -= Time.deltaTime;
                if (nextWaveSpawnTimer <= 0f)
                {
                    SpawnWave();
                }
                break;
            case  State.SpawningWave:
                if (remainingEnemySpawnAmount > 0)
                {
                    nextEnemySpawnTimer -= Time.deltaTime;
                    if (nextEnemySpawnTimer <= 0f)
                    {
                        nextEnemySpawnTimer = Random.Range(0f, .2f);
                        Enemy.Create(spawnPosition + UtilsClass.GetRandomDir() * Random.Range(0f, 10f));
                        remainingEnemySpawnAmount--;
                        if (remainingEnemySpawnAmount <= 0)
                        {
                            state = State.WaitingToSpawnNextWave;
                            spawnPosition = spawnPositionTransform[Random.Range(0,spawnPositionTransform.Count -1)].position;
                            nextWaveSpawnPositionTransform.position = spawnPosition;
                            nextWaveSpawnTimer = 10f;
                        }
                    }
                }
                break;
        }
        

    }

    private void SpawnWave()
    {
        remainingEnemySpawnAmount = 8+ waveNumber * 5 ;
        state = State.SpawningWave;
        waveNumber++;
        OnWaveNumberChanged?.Invoke(this,EventArgs.Empty);
    }


    public int GetWaveNumber()
    {
        return waveNumber;
    }

    public float GetNextWaveSpawnTimer()
    {
        return nextWaveSpawnTimer;
    }

    public Vector3 GetEnemySpawnPosition()
    {
        return spawnPosition;
    }
}

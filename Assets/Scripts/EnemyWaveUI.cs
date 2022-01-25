using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyWaveUI : MonoBehaviour
{
    [SerializeField] private EnemyWaveManager enemyWaveManager;
    private TextMeshProUGUI waveNumberText, waveMessageText;
    private RectTransform enemyWaveSpawnPositionIndictor;
    private RectTransform enemyCloestPositionIndicator;
    private Camera mainCamera;
    private void Awake()
    {
        waveNumberText = transform.Find("WaveNumberText").GetComponent<TextMeshProUGUI>();
        waveMessageText = transform.Find("WaveMessageText").GetComponent<TextMeshProUGUI>();
        enemyWaveSpawnPositionIndictor=transform.Find("EnemyWaveSpawnPositionIndicator").GetComponent<RectTransform>();
        enemyCloestPositionIndicator = transform.Find("EnemyCloestPositionIndicator").GetComponent<RectTransform>();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        float nextWaveSpawnTimer = enemyWaveManager.GetNextWaveSpawnTimer();
        if (nextWaveSpawnTimer < 0f)
        {
            SetMessageText("");
        }
        else
        {
            SetMessageText("next wave in " + nextWaveSpawnTimer.ToString("F0") + "s");
        }
        HandleNextWaveSpawnPositionIndicator();
        HandleCloestEnemyPositionIndicator();
    }

    private void HandleNextWaveSpawnPositionIndicator()
    {
        
        Vector3 dirToNextSpawnPosition = (enemyWaveManager.GetEnemySpawnPosition() - mainCamera.transform.position).normalized;
        enemyWaveSpawnPositionIndictor.anchoredPosition = dirToNextSpawnPosition * 350f;
        enemyWaveSpawnPositionIndictor.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVector(dirToNextSpawnPosition));

        float distanceToNextSpawnPosition =
            Vector3.Distance(enemyWaveManager.GetEnemySpawnPosition(), mainCamera.transform.position);
        enemyWaveSpawnPositionIndictor.gameObject.SetActive(distanceToNextSpawnPosition > mainCamera.orthographicSize * 1.7f);
    }
    private void HandleCloestEnemyPositionIndicator()
    {
        Enemy cloestEenemy = LookForTargets();
        if (cloestEenemy!= null)
        {
            Vector3 dirCloestEnemyPosition = (cloestEenemy.transform.position - mainCamera.transform.position).normalized;
            enemyCloestPositionIndicator.anchoredPosition = dirCloestEnemyPosition * 450f;
            enemyCloestPositionIndicator.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVector(dirCloestEnemyPosition));
            float distanceToCloestEnemy =
                Vector3.Distance(cloestEenemy.transform.position, mainCamera.transform.position);
            enemyCloestPositionIndicator.gameObject.SetActive(distanceToCloestEnemy > mainCamera.orthographicSize * 1.7f); 
        }
        else
        {
            enemyCloestPositionIndicator.gameObject.SetActive(false);  
        } 
    }
    
    private Enemy LookForTargets()
    {
        float targetMaxRadius = 299999999f;
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(mainCamera.transform.position, targetMaxRadius);
        Enemy targetEnemy = null;
        foreach (var collider2D in collider2DArray)
        {
            Enemy enemy = collider2D.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                if (targetEnemy == null)
                {
                    targetEnemy = enemy;
                }
                else
                {
                    if (Vector3.Distance(transform.position, enemy.transform.position) <
                        Vector3.Distance(transform.position, targetEnemy.transform.position))
                    {
                        Debug.Log("发现乐凯敌人");
                        targetEnemy = enemy;
                    }
                }
            }
        }
        return targetEnemy;
    }
    private void Start()
    {
        enemyWaveManager.OnWaveNumberChanged += EnemyWaveUI_OnWaveNumberChanged;
        SetWaveNumberText("Wave " + enemyWaveManager.GetWaveNumber() + " ");
    }

    private void EnemyWaveUI_OnWaveNumberChanged(object sedner, EventArgs e)
    {
        SetWaveNumberText("Wave " + enemyWaveManager.GetWaveNumber() + " ");
    }

    private void SetMessageText(string message)
    {
        waveMessageText.SetText(message);
    }

    private void SetWaveNumberText(string text)
    {
        waveNumberText.SetText(text);
    }
}

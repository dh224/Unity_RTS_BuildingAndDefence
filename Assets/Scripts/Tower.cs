using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private float shootTimer;
    [SerializeField]private float shootTimerMax;
    private Enemy targetEnemy;
    private float lookforTargetTimer= 0f;
    private float lookforTargetTimerMax = 0.3f;
    private Vector3 projectileSpwanPosition;

    private void Awake()
    {
        projectileSpwanPosition = transform.Find("ProjectileSpawnPosition").position;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleTargeting();
        HandleShooting();
    }

    private void HandleShooting()
    {
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0f)
        {
            shootTimer += shootTimerMax;
            if (targetEnemy != null)
            {
                ArrowProjectile.Create(projectileSpwanPosition, targetEnemy);
            }
        }
    }
    private void LookForTargets()
    {
        float targetMaxRadius = 20f;
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, targetMaxRadius);
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
    }
    
    
    private void HandleTargeting()
    {
        lookforTargetTimer -= Time.deltaTime;
        if (lookforTargetTimer <= 0f)
        {
            lookforTargetTimer += lookforTargetTimerMax;
            LookForTargets();
        } 
    }
}

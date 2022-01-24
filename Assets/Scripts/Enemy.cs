using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Random = UnityEngine.Random;


public class Enemy : MonoBehaviour
{

    public static Enemy Create(Vector3 position)
    {
        Transform pf_Enemy = Resources.Load<Transform>("PF_Enemy");
        Transform newEnemyTransform = Instantiate(pf_Enemy, position, Quaternion.identity);
        Enemy enemy = newEnemyTransform.GetComponent<Enemy>();
        return enemy;
    }
    
    
    
    private Transform targetTransform;
    private float lookforTargetTimer;
    private float lookforTargetTimerMax = 0.5f;
    private Rigidbody2D rigidbody2D;

    private HealthSystem healthSystem;
    // Start is called before the first frame update
    void Start()
    {
        targetTransform  =  BuildingManager.Instance.GetHQBuilding().transform;
        rigidbody2D = GetComponent<Rigidbody2D>();
        lookforTargetTimer = Random.Range(0f, lookforTargetTimerMax);
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.OnDied += healthSystem_OnDied;
    }

    private void healthSystem_OnDied(object sender, EventArgs e)
    {
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleTargeting();
    }

    private void HandleTargeting()
    {
        if (lookforTargetTimer < 0f)
        {
            lookforTargetTimer += lookforTargetTimerMax;
            LookForTargets();
        } 
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
       Building building = col.gameObject.GetComponent<Building>();
       if (building != null)
       {
           HealthSystem healthSystem = building.GetComponent<HealthSystem>();
           healthSystem.Damage(10);
           Destroy(gameObject);
       }
    }

    private void HandleMovement()
    {
        if (targetTransform != null)
        {
            Vector3 moveDir = (targetTransform.position - transform.position).normalized; 
            float moveSpeed = 5f;
            rigidbody2D.velocity = moveSpeed * moveDir;
            lookforTargetTimer -= Time.deltaTime;
        }
        else
        {
            LookForTargets();
            rigidbody2D.velocity = Vector2.zero;
        }
    }
    private void LookForTargets()
    {
        float targetMaxRadius = 15f;
        Collider2D[] collider2DArray  = Physics2D.OverlapCircleAll(transform.position,targetMaxRadius);
        foreach (var collider2D in collider2DArray)
        {
            Building building = collider2D.gameObject.GetComponent<Building>();
            if (building != null)
            {
                if (targetTransform == null)
                {
                    targetTransform = building.transform;
                }
                else
                {
                    if (Vector3.Distance(transform.position, building.transform.position) <
                        Vector3.Distance(transform.position, targetTransform.position))
                    {
                        targetTransform = building.transform;
                    }
                }
            }
        }
        if (targetTransform == null)
        {
            targetTransform = BuildingManager.Instance.GetHQBuilding().transform;
        }
    }
}

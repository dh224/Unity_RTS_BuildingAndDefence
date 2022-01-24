using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowProjectile : MonoBehaviour
{
    // Start is called before the first frame update
    private Enemy targetEnemy;
    private Vector3 lastMoveDir;
    private float timeToDie = 1f;
    private void SetTarget(Enemy enemy)
    {
        targetEnemy = enemy;
    }
    
    public static ArrowProjectile Create(Vector3 position,Enemy targetEnemy)
    {
        Transform pf_ArrowProjectile = Resources.Load<Transform>("PF_ArrowProjectile");
        Transform arrowprojectileTransform = Instantiate(pf_ArrowProjectile, position, Quaternion.identity);
        ArrowProjectile arrowProjectile = arrowprojectileTransform.GetComponent<ArrowProjectile>();
        arrowProjectile.SetTarget(targetEnemy);
        return arrowProjectile;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDir;
        if (targetEnemy != null)
        {
            moveDir = (targetEnemy.transform.position - transform.position).normalized;
            transform.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVector(moveDir));
            lastMoveDir = moveDir;
        }
        else
        {
            moveDir = lastMoveDir;
        }
        float moveSpeed = 15f;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
        timeToDie -= Time.deltaTime;
        if (timeToDie <= 0f)
        {
            
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    { 
        Enemy enemy = col.GetComponent<Enemy>();
        if (enemy != null)
        {
            int damageAmount = 10;
            enemy.GetComponent<HealthSystem>().Damage(damageAmount);
            Destroy(gameObject);
        }
    }
}

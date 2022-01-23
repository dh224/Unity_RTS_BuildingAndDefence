using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private HealthSystem healthSystem;
    // Start is called before the first frame update
    private Transform barTransform;
    private Transform barBackgroundTransform;
    private Transform background;

    private void Awake()
    {
        barTransform = transform.Find("Bar");
        barBackgroundTransform = transform.Find("BarBackground");
        background = transform.Find("Background");
    }

    private void UpdateBar()
    {
        barTransform.localScale = new Vector3(healthSystem.GetHealthAmountNormalized(), 1, 1);
    }

    void Start()
    {
        healthSystem.OnDamage += healthSystem_OnDamaged;
        UpdateBar();
        UpdateHealthBarVisiable();
    }
    private void healthSystem_OnDamaged(object sender, EventArgs e)
    {
        UpdateBar();
        UpdateHealthBarVisiable();
    }
    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKey(KeyCode.LeftAlt))
        {
            barTransform.gameObject.SetActive(true);
            barBackgroundTransform.gameObject.SetActive(true);
            background.gameObject.SetActive(true);
        }
        else
        {
            barTransform.gameObject.SetActive(false);
            barBackgroundTransform.gameObject.SetActive(false);
            background.gameObject.SetActive(false);
        }*/
    }

    private void UpdateHealthBarVisiable()
    {
        if (healthSystem.ISFullHealth())
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}

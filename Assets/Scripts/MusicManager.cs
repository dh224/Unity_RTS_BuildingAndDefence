using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static MusicManager Instance;
    private float volume = 0.5f;
    private AudioSource audioSource;


    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = volume;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void IncreaseVolume()
    {
        volume += 0.1f;
        if (volume >= 1f)
        {
            volume = 1;
        }

        audioSource.volume = volume;
    }

    public void DecreaseVolume()
    {
        volume -= 0.1f;
        if (volume <= 0f)
        {
            volume = 0f;
        }
        audioSource.volume = volume;
    }

    public float GetVolume()
    {
        return volume;
    }
}

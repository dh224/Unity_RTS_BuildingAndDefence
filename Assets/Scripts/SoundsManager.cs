using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    public static SoundsManager Instance { get; private set; }
    private float volume = 0.5f;
    public enum Sound
    {
        BuildingPlaced,
        BuildingDamaged,
        BuildingDestroyed,
        EnemyDie,
        EnemyHit,
        GameOver,
    }
    private  AudioSource audioSource;
    private Dictionary<Sound, AudioClip> soundsDictionary;
    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
        soundsDictionary = new Dictionary<Sound, AudioClip>();
        foreach (Sound sound in System.Enum.GetValues(typeof(Sound)))
        {
            soundsDictionary[sound] = Resources.Load<AudioClip>(sound.ToString());
        }
    }
    public void PlaySound(Sound sound)
    {
        audioSource.PlayOneShot(soundsDictionary[sound],volume);
    }

    public void IncreaseVolume()
    {
        volume += 0.1f;
        if (volume >= 1f)
        {
            volume = 1;
        }
    }

    public void DecreaseVolume()
    {
        volume -= 0.1f;
        if (volume <= 0f)
        {
            volume = 0f;
        }
    }

    public float GetVolume()
    {
        return volume;
    }
}

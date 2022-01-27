using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    public static SoundsManager Instance { get; private set; }
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
        audioSource.PlayOneShot(soundsDictionary[sound]);
    }
}

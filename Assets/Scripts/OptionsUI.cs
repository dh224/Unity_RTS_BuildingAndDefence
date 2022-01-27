using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private SoundsManager soundsManager;
    [SerializeField] private MusicManager musicManager;

    private TextMeshProUGUI soundValText;
    private TextMeshProUGUI musicValText;
    public void TriggerVisiable()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        if (gameObject.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
    private void Awake()
    {
        soundValText = transform.Find("SoundValText").GetComponent<TextMeshProUGUI>();
        musicValText = transform.Find("MusicValText").GetComponent<TextMeshProUGUI>();
        
        transform.Find("SoundPlusBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            SoundsManager.Instance.IncreaseVolume();
            UpdateText();
        });
        transform.Find("SoundMinusBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            SoundsManager.Instance.DecreaseVolume(); 
            UpdateText();

        });
        transform.Find("MusicPlusBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            MusicManager.Instance.IncreaseVolume();
            UpdateText();
        });
        transform.Find("MusicMinusBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            MusicManager.Instance.DecreaseVolume();
            UpdateText();
        });
        transform.Find("MainMenuBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            Time.timeScale = 1f;
            GameSceneManager.Load(GameSceneManager.Scene.MainMenuScene);
        });
        
    }

    private void UpdateText()
    {
        soundValText.SetText(Mathf.RoundToInt(soundsManager.GetVolume() * 10f).ToString());
        musicValText.SetText(Mathf.RoundToInt(musicManager.GetVolume() * 10f).ToString());
    }

    void Start()
    {
        UpdateText();
        gameObject.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}

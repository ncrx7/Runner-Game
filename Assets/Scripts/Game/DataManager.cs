using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }
    public int BestGeneralScore { get; private set;}
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        BestGeneralScore = LoadBestGeneralScore("BestGeneralScore");
        //Debug.Log("BestGeneralScore: " + BestGeneralScore);
    }
    private void OnEnable()
    {
        EventSystem.OnDie += SaveBestGeneralScore;
    }

    private void OnDisable()
    {
        EventSystem.OnDie -= SaveBestGeneralScore;
    }

    private void SaveBestGeneralScore()
    {
        if (BestGeneralScore < GameManager.Instance.GeneralScore)
        {
            PlayerPrefs.SetInt("BestGeneralScore", GameManager.Instance.GeneralScore);
            PlayerPrefs.Save();
            Debug.Log("Veri kaydedildi: " + "BestGeneralScore" + " = " + GameManager.Instance.GeneralScore);
        }
    }

    public int LoadBestGeneralScore(string key)
    {
        int value = PlayerPrefs.GetInt(key);
        //Debug.Log("Veri yüklendi: " + key + " = " + value);
        return value;
    }
}

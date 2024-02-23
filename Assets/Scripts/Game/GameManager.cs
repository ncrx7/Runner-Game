using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region fields
    public static GameManager Instance { get; private set; }
    [field: SerializeField] public float Health { get; set; } = 3f;
    public float TimeScore { get; private set; }
    public int CoinScore { get; private set; }
    public int GeneralScore { get; private set; }
    [field: SerializeField] public int StunTime { get; private set; } = 2;
    public bool reachedBestScore = false;
    public bool isDead = false;
    public bool stunActive = false;

    [SerializeField] public float PlatformSpeed { get; private set; } = 3f;
    #endregion

    #region mb callback functions
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

    private void Update()
    {
        //Debug.Log("score: " + (int)score);
        if (isDead)
        {
            return;
        }

        IncreaseScore();
        CalculateGeneralScore();
        UpdatePlatformSpeed();

         if(CheckBestScoreReached() && !reachedBestScore)
        {
            reachedBestScore = true;
            EventSystem.OnReachedBestScore?.Invoke(Color.green);
        } 
    }
    #endregion

    #region methods
    private void IncreaseScore()
    {
        TimeScore += 1.5f * Time.deltaTime;
        EventSystem.UpdateTimeScore?.Invoke();
    }
    public void IncreaseCoinAmount()
    {
        CoinScore++;
    }
    private void UpdatePlatformSpeed()
    {
        PlatformSpeed += Time.deltaTime * 0.1f;
    }
    public void DecreaseHealth()
    {
        if (Health <= 0)
        {
            return;
        }

        Health--;

        if (Health == 0)
        {
            isDead = true;
            EventSystem.OnDie?.Invoke();
        }
    }
    private void CalculateGeneralScore()
    {
        GeneralScore = (int)TimeScore + CoinScore;
    }
    public bool CheckBestScoreReached()
    {
        if (GeneralScore > DataManager.Instance.BestGeneralScore)
        {
            return true;
        }
        return false;
    }
    public void ReloadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    #endregion
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timeScoreText;
    [SerializeField] private TextMeshProUGUI _coinScoreText;
    [SerializeField] private TextMeshProUGUI _generalScoreText;
    [SerializeField] private TextMeshProUGUI _bestScoreText;
    [SerializeField] private TextMeshProUGUI _bestScoreOnDieUIText;
    [SerializeField] private Image _healthBar;
    [SerializeField] private GameObject _deadUI;

    private void OnEnable()
    {
        EventSystem.UpdateCoinScore += UpdateCoinScoreHandler;
        EventSystem.UpdateTimeScore += UpdateTimeScoreHandler;
        EventSystem.OnDie += ActivateDeadUI;
        EventSystem.OnDie += UpdateGeneralScoreHandler;
        EventSystem.OnDie += CloseAliveGameUI;
        EventSystem.OnReachedBestScore += ChangeTextsColor;
        EventSystem.OnReachedBestScore += ActivateBestScoreText;
        EventSystem.DamageTaken += UpdateHealthBarUI;
    }

    private void OnDisable()
    {
        EventSystem.UpdateCoinScore -= UpdateCoinScoreHandler;
        EventSystem.UpdateTimeScore -= UpdateTimeScoreHandler;
        EventSystem.OnDie -= ActivateDeadUI;
        EventSystem.OnDie -= UpdateGeneralScoreHandler;
        EventSystem.OnDie -= CloseAliveGameUI;
        EventSystem.OnReachedBestScore -= ChangeTextsColor;
        EventSystem.OnReachedBestScore -= ActivateBestScoreText;
        EventSystem.DamageTaken -= UpdateHealthBarUI;
    }
    private void UpdateCoinScoreHandler()
    {
        _coinScoreText.text = "Coin Amount: " + GameManager.Instance.CoinScore.ToString();
    }

    private void UpdateTimeScoreHandler()
    {
        _timeScoreText.text = "Time Score: " + GameManager.Instance.TimeScore.ToString("F0");
    }

    private void UpdateGeneralScoreHandler()
    {
        _generalScoreText.text = GameManager.Instance.GeneralScore.ToString("F0");
    }

    private void ActivateDeadUI()
    {
        _deadUI.SetActive(true);
        if (GameManager.Instance.reachedBestScore)
        {
            _bestScoreOnDieUIText.text = GameManager.Instance.GeneralScore.ToString();
        }
        else
        {
            _bestScoreOnDieUIText.text = DataManager.Instance.BestGeneralScore.ToString();
        }
    }

    private void UpdateHealthBarUI()
    {
        float healthAspect = GameManager.Instance.Health / 3f;
 
        _healthBar.fillAmount = healthAspect;
        _healthBar.color = Color.Lerp(Color.red, Color.green, healthAspect);
    }
    private void CloseAliveGameUI()
    {
        _timeScoreText.enabled = false;
        _coinScoreText.enabled = false;
    }

    private void ChangeTextsColor(Color color)
    {
        _timeScoreText.color = color;
        _coinScoreText.color = color;
    }

    private void ActivateBestScoreText(Color color)
    {
        _bestScoreText.enabled = true;
    }
}

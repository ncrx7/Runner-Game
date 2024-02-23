using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        IncreaseCoinScore();
        PlayCoinCollectAnimation();
        DestroyCoin();
        AudioManager.Instance.PlayEffectSound(AudioManager.Instance._coinCollectSoundEffect);
    }

    private void IncreaseCoinScore()
    {
        GameManager.Instance.IncreaseCoinAmount();
        EventSystem.UpdateCoinScore?.Invoke();
    }

    private void PlayCoinCollectAnimation()
    {

    }

    private void DestroyCoin()
    {
        Destroy(this.gameObject);
    }
}

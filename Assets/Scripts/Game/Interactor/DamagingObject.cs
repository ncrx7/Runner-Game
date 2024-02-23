using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class DamagingObject : MonoBehaviour, IInteractable
{
    private Rigidbody _rb;
    private Collider _col;
    private PlayerController _playerController;

    public void Interact()
    {
        Damage();
        PlayStunAnimation();
        Stun();
        AudioManager.Instance.PlayEffectSound(AudioManager.Instance._stunSoundEffect);
        EventSystem.DamageTaken?.Invoke();
        //Debug.Log("health: " + GameManager.Instance.Health);
    }

    private void Damage()
    {
        GameManager.Instance.DecreaseHealth();
    }

    private void Stun()
    {
        if (!GameManager.Instance.stunActive)
        {
            GameManager.Instance.stunActive = true;
            EventSystem.OnStunEffect?.Invoke();
        }
    }

    private void PlayStunAnimation()
    {
        StartCoroutine(PlayStunAnimationCoroutine());
    }

    IEnumerator PlayStunAnimationCoroutine()
    {
        AnimationController.Instance.SetAnimatorBoolParameter("isStunned", true);

        yield return new WaitForSecondsRealtime(2);

        if (!GameManager.Instance.isDead)
        {
            AnimationController.Instance.SetAnimatorBoolParameter("isStunned", false);
        }
    }




}

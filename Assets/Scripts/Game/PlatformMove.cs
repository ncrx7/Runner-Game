using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlatformMove : MonoBehaviour
{
    private void FixedUpdate()
    {
        if(!GameManager.Instance.stunActive && !GameManager.Instance.isDead)
        {
            MovePlatformVerticalBack();
        }
    }

    private void OnEnable()
    {
        EventSystem.OnStunEffect += MovePlatformVerticalForward;
    }
    private void OnDisable()
    {
        EventSystem.OnStunEffect -= MovePlatformVerticalForward;
    }
    private void MovePlatformVerticalBack()
    {
        transform.Translate(-Vector3.forward * GameManager.Instance.PlatformSpeed * Time.fixedDeltaTime);
    }
    private void MovePlatformVerticalForward() //sersemleme aktif oldugunda
    {
        if(!GameManager.Instance.isDead)
            StartCoroutine(MovePlatformVerticalBackCoroutine());
    }
    IEnumerator MovePlatformVerticalBackCoroutine()
    {
        transform.DOMove(transform.position + Vector3.forward * 5f, GameManager.Instance.StunTime);
        yield return new WaitForSecondsRealtime(GameManager.Instance.StunTime);
        GameManager.Instance.stunActive = false;
    }
}

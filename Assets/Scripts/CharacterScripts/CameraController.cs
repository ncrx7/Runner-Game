using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject _cameraTargetObject;
    private Vector3 _cameraOffset;
    private void OnEnable()
    {
        EventSystem.OnPressedSwipeKey += HandleCameraPosition;
    }
    private void OnDisable()
    {
        EventSystem.OnPressedSwipeKey -= HandleCameraPosition;
    }
    private void Start()
    {
        _cameraOffset = _cameraTargetObject.transform.position - transform.position;
    }
     private void LateUpdate()
    {
        transform.position = _cameraTargetObject.transform.position - _cameraOffset;
    } 

    private void HandleCameraPosition(int i)
    {
        Vector3 targetPosition = _cameraTargetObject.transform.position - _cameraOffset;
        //transform.DOMove(targetPosition, 0.2f).SetEase(Ease.Linear);
    }
}

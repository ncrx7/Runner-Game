using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordMove : MonoBehaviour
{
    Vector3 _initialPosition;
    private void OnEnable()
    {
        EventSystem.GeneratePlatform += ResetPosition;
    }
    private void OnDisable()
    {
        EventSystem.GeneratePlatform -= ResetPosition;
    }
    private void Start()
    {
        _initialPosition = transform.position;
    }
    void Update()
    {
        MoveSwordVerticalBack();
    }

    private void MoveSwordVerticalBack()
    {
        Vector3 dir = -transform.forward * 5 * Time.fixedDeltaTime;
        //Debug.Log("dir: " + dir);
        transform.Translate(dir);
    }

    private void ResetPosition()
    {
        transform.position = _initialPosition;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInputManager : MonoBehaviour
{
    private int _inputId;
    public bool isInteracting;
    public static CharacterInputManager Instance { get; private set; }

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

    void Update()
    {
        HandleInput(); //klavye tuşlarını dinliyoruz
    }

    private void HandleInput() //tuşlara göre eventi çalıştırma
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _inputId = 0;
            EventSystem.OnPressedSwipeKey?.Invoke(_inputId);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _inputId = 1;
            EventSystem.OnPressedSwipeKey?.Invoke(_inputId);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _inputId = 2;
            EventSystem.OnPressedSwipeKey?.Invoke(_inputId);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _inputId = 3;
            EventSystem.OnPressedSwipeKey?.Invoke(_inputId);
        }
    }

/*     private void HandleInteractingState()
    {
        isInteracting = AnimationController.Instance.animator.GetBool("isInteracting");
    } */
}

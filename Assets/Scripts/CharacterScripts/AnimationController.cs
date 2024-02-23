using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public static AnimationController Instance { get; private set; }
    //[SerializeField] private CharacterInputManager _inputManager;
    public Animator animator;

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

    public void PlayTargetAnimation(string targetAnim, bool isInteracting)
    {
        animator.applyRootMotion = isInteracting;
        animator.SetBool("isInteracting", isInteracting);
        animator.CrossFade(targetAnim, 0f);
    }

    public void SetAnimatorBoolParameter(string parameterName, bool parameterValue)
    {
        animator.SetBool(parameterName, parameterValue);
    }

    public bool GetIsInteracting()
    {
        return animator.GetBool("isInteracting");
    }
    private void OnAnimatorMove()
    {
         if (CharacterInputManager.Instance.isInteracting == false)
        {
            return;
        } 
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateColliderManager : MonoBehaviour, IInteractable
{
    private bool _isGenerated;
    public void Interact()
    {
        if(!_isGenerated)
        {
            StartCoroutine(GenerateWait());
            EventSystem.GeneratePlatform?.Invoke();
            Debug.Log("platform is regenerating..");
        }
    }

    IEnumerator GenerateWait()
    {
        _isGenerated = true;
        yield return new WaitForSecondsRealtime(5f);
        _isGenerated = false;
    }

}

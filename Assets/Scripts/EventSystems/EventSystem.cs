using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EventSystem : MonoBehaviour
{
    public static Action<int> OnPressedSwipeKey; 
    public static Action GeneratePlatform;
    public static Action OnStunEffect;
    public static Action OnDie;
    public static Action<Color> OnReachedBestScore;
    public static Action DamageTaken;

    #region UI events
    public static Action UpdateCoinScore;
    public static Action UpdateTimeScore;
    #endregion

}

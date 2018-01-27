using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfectedTroopView : MonoBehaviour
{
    [SerializeField]
    private Image Fill;
    [SerializeField]
    private float DelayBeforeChange;

    [HideInInspector]
    public int IndexOfInfectedTroop;

    private float timer;
    private bool isInitialized = false;
    private Action<int> pressCallback;

    private void Update()
    {
        if(isInitialized)
        {
            timer += Time.deltaTime;
            Fill.fillAmount = Mathf.Clamp01(timer / DelayBeforeChange);
            if (timer >= DelayBeforeChange)
            {
                OnTroopIconPressed();
                GameManager.Instance.InfectRandomTroop();
                isInitialized = false;
            }
        }
    }

    public void Init(int index)
    {
        isInitialized = true;
        IndexOfInfectedTroop = index;
        timer = 0;
        gameObject.SetActive(true);
        Update();
    }

    public void SetCallback(Action<int> troopViewPressedCallback)
    {
        pressCallback = troopViewPressedCallback;
    }
    
    public void OnTroopIconPressed()
    {
        if(pressCallback != null)
        {
            pressCallback(IndexOfInfectedTroop);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfectedTroopView : MonoBehaviour
{
    [SerializeField]
    private Image Fill;

    [HideInInspector]
    public int IndexOfInfectedTroop;

    private float timer;
    private bool isInitialized = false;
    private Action<int> pressCallback;

    private float InfectionCooldown { get { return SettingsService.GameSettings.InfectionCooldown; } }

    private void Update()
    {
        if(isInitialized)
        {
            timer += Time.deltaTime;
            Fill.fillAmount = Mathf.Clamp01(timer / InfectionCooldown);
            if (timer >= InfectionCooldown)
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

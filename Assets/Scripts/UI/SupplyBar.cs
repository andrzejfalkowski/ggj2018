using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SupplyBar : MonoBehaviour
{
    //[SerializeField]
    //private SupplyDropButton SupplyDropButton;

    [SerializeField]
    private Image progressBar;

    public void UpdateProgressBar(float normalizedValue)
    {
        progressBar.fillAmount = normalizedValue;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SupplyBar : MonoBehaviour
{
    [SerializeField]
    private GameObject supplyDropButtonPrefab;

    [SerializeField]
    private Transform supplyDropButtonParent;
    [SerializeField]
    private Image progressBar;

    private List<SupplyButton> supplyButtons = new List<SupplyButton>();

    public void Init(List<BaseSupplyDefinition> definitions)
    {
        supplyButtons.Clear();
        float parentWidth = supplyDropButtonParent.GetComponent<RectTransform>().rect.width;

        for (int i = 0; i < definitions.Count; i++)
        {
            GameObject go = GameObject.Instantiate(supplyDropButtonPrefab, supplyDropButtonParent);
            SupplyButton button = go.GetComponentInChildren<SupplyButton>();

            button.Init(definitions[i]);
            supplyButtons.Add(button);

            float x = definitions[i].SupplyCost * parentWidth - (parentWidth / 2f);
            go.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, 0f);
        }
    }

    public void UpdateProgressBar(float normalizedValue)
    {
        progressBar.fillAmount = normalizedValue;

        for (int i = 0; i < supplyButtons.Count; i++)
        {
            supplyButtons[i].Refresh(normalizedValue);
        }
    }
}

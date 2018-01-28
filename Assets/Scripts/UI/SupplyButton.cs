using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SupplyButton : MonoBehaviour 
{
    [SerializeField]
    private Button button;
    [SerializeField]
    private Text text;
    [SerializeField]
    private Image image;

    private BaseSupplyDefinition supplyDefinition;

    public void Init(BaseSupplyDefinition definition)
    {
        supplyDefinition = definition;

        if (text != null)
        {
            text.text = definition.Title;
        }

        if (image != null)
        {
            image.sprite = definition.Icon;
        }
    }

    public void Refresh(float normalizedSupplyScore)
    {
        button.interactable = supplyDefinition != null && supplyDefinition.SupplyCost <= normalizedSupplyScore;
    }

    public void TriggerSupplyDrop()
    {
        SupplyManager.Instance.TriggerSupplyDrop(supplyDefinition);
    }
}

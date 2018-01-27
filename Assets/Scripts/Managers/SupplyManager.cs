using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseSupplyDefinition
{
    public enum ESupplyType
    {
        WEAPONZ,
        HEALZ,
        DEATH_FROM_ABOVE,
        TURRETZ,
        GODMODE
    }

    public Sprite Icon;
    public string Title;

    public ESupplyType SupplyType;
    public float SupplyCost = 0.5f;
    public float Cooldown = 0f;
    public float ArrivalTime = 3f;
}

public class SupplyManager : MonoBehaviour 
{
    public static SupplyManager Instance = null;

    [SerializeField]
    private SupplyBar SupplyBar;

    [Header("Parameters")]
    [SerializeField]
    private float maxSupplyScore = 100f;
    [SerializeField]
    private float startSupplyScore = 0f;

    private float supplyScore;

    [SerializeField]
    private List<BaseSupplyDefinition> SupplyDefinitions;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    // called from GameManager
    public void InitializeGame()
    {
        SetSupplyScore(startSupplyScore);
        SupplyBar.Init(SupplyDefinitions);
    }

    public void TriggerSupplyDrop(BaseSupplyDefinition supply)
    {
        if (supplyScore * maxSupplyScore < supply.SupplyCost)
        {
            Debug.LogError("Supply triggered without supply score!");
            return;
        }
         
        ChangeSupplyScore(-(supply.SupplyCost * maxSupplyScore));
        // TODO: handle supply logic
        switch(supply.SupplyType)
        {
            default:
                break;
        }
    }

    public void ChangeSupplyScore(float change)
    {
        SetSupplyScore(supplyScore + change);
    }

    private void SetSupplyScore(float value)
    {
        supplyScore = value;
        SupplyBar.UpdateProgressBar(supplyScore / maxSupplyScore);
    }
}

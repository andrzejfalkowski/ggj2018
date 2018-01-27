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
    [SerializeField]
    private Transform supplyPickupsParent;
    [SerializeField]
    private GameObject supplyPickupPrefab;

    [Header("Parameters")]
    [SerializeField]
    private float maxSupplyScore = 100f;
    [SerializeField]
    private float startSupplyScore = 0f;
    [SerializeField]
    private float lastMovePointRange = 5f;

    private float supplyScore;

    [SerializeField]
    private List<BaseSupplyDefinition> SupplyDefinitions;

    private Vector2 lastMovePoint = Vector2.zero;
    private Dictionary<BaseSupplyDefinition, float> suppliesToSpawn = new Dictionary<BaseSupplyDefinition, float>();

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

        GameObject go = GameObject.Instantiate(supplyPickupPrefab, supplyPickupsParent);
        go.GetComponentInChildren<Pickup>().Init(supply);

        Vector2 spawnCandidate = GetSpawnCandidate();
        int safety = 0;
        int layerMask = 1 << LayerMask.NameToLayer("Blockers");
        while(safety < 10 && Physics.Raycast(new Vector3(spawnCandidate.x, spawnCandidate.y, 1f), new Vector3(0f, 0f, -1f), layerMask))
        {
            spawnCandidate = GetSpawnCandidate();
            Debug.Log(spawnCandidate);
            safety++;
        }
        go.transform.localPosition = spawnCandidate;
    }

    Vector2 GetSpawnCandidate()
    {
        float randomRange = UnityEngine.Random.Range(0f, lastMovePointRange);
        float x = UnityEngine.Random.Range(-randomRange, randomRange);
        float y = Mathf.Sqrt((randomRange * randomRange) - (x * x));
        return new Vector2(x, y);
    }

    public void TriggerSupplyPickup(BaseSupplyDefinition supply)
    {
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
        supplyScore = Mathf.Clamp(value, 0f, maxSupplyScore);
        SupplyBar.UpdateProgressBar(supplyScore / maxSupplyScore);
    }

    public void UpdateLastMovePoint(Vector2 value)
    {
        lastMovePoint = value;
    }

//    public void Update()
//    {
//        foreach (var pair in suppliesToSpawn)
//        {
//            
//        }
//    }
}

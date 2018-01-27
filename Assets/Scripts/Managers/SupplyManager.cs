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
    struct SupplyToDrop
    {
        public BaseSupplyDefinition BaseSupplyDefinition;
        public float Time;
    }

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
    private List<SupplyToDrop> suppliesToDrop = new List<SupplyToDrop>();

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

        suppliesToDrop.Add(new SupplyToDrop()
            {
                BaseSupplyDefinition = supply,
                Time = supply.ArrivalTime

        });
    }

    Vector2 GetSpawnCandidate()
    {
        float randomRange = UnityEngine.Random.Range(0f, lastMovePointRange);
        float x = UnityEngine.Random.Range(-randomRange, randomRange);
        float y = Mathf.Sqrt((randomRange * randomRange) - (x * x));
        return new Vector2(x + lastMovePoint.x, y + lastMovePoint.y);
    }

    public void TriggerSupplyPickup(BaseSupplyDefinition supply)
    {
        // TODO: handle supply logic
        switch(supply.SupplyType)
        {
            case BaseSupplyDefinition.ESupplyType.WEAPONZ:
                GameManager.Instance.DoubleDamage();
                break;
            case BaseSupplyDefinition.ESupplyType.HEALZ:
                GameManager.Instance.CureAll();
                break;
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

    public void Update()
    {
        for (int i = 0; i < suppliesToDrop.Count; i++)
        {
            SupplyToDrop std = suppliesToDrop[i];
            std.Time -= Time.deltaTime;
            suppliesToDrop[i] = std;
            if (suppliesToDrop[i].Time <= 0f)
            {
                GameObject go = GameObject.Instantiate(supplyPickupPrefab, supplyPickupsParent);
                go.GetComponentInChildren<Pickup>().Init(suppliesToDrop[i].BaseSupplyDefinition);

                Vector2 spawnCandidate = GetSpawnCandidate();
                int safety = 0;
                int layerMask = 1 << LayerMask.NameToLayer("Blockers");
                while(safety < 10 && Physics.Raycast(new Vector3(spawnCandidate.x, spawnCandidate.y, 1f), new Vector3(0f, 0f, -1f), layerMask))
                {
                    spawnCandidate = GetSpawnCandidate();
                    safety++;
                }
                go.transform.localPosition = spawnCandidate;

                suppliesToDrop.RemoveAt(i);
                i--;
            }
        }
    }
}

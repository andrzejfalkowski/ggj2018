using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private BaseSupplyDefinition supplyDefinition;

    public void Init(BaseSupplyDefinition _supplyDefinition)
    {
        supplyDefinition = _supplyDefinition;
    }

    void OnTriggerEnter(Collider other)
    {
        AnimatedCharacter ac = other.transform.GetComponentInChildren<AnimatedCharacter>();
        if (ac != null && ac.Owner != null && (ac.Owner as PlayerTroop) != null)
        {
            SupplyManager.Instance.TriggerSupplyPickup(supplyDefinition);
            Destroy(this.gameObject);
        }
    }
}

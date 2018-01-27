using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadDisplay : MonoBehaviour
{
    [SerializeField]
    private InfectedTroopView TroopViewPrefab;

    private List<InfectedTroopView> infectedTroops;

    public void Init(int troopCount)
    {
        infectedTroops = new List<InfectedTroopView>();
        for (int i = 0; i < troopCount; i++)
        {
            InfectedTroopView troopView = Instantiate<InfectedTroopView>(TroopViewPrefab);
            troopView.transform.SetParent(transform);
            troopView.transform.localScale = Vector3.one;
            troopView.gameObject.SetActive(false);
            troopView.SetCallback(OnInfectedTroopViewPressed);
            infectedTroops.Add(troopView);
        }
    }

    public void ShowInfectedTroop(int troopIndex)
    {
        infectedTroops[troopIndex].Init(troopIndex);
    }

    public void HideInfectedTroop(int troopIndex)
    {
        infectedTroops[troopIndex].gameObject.SetActive(false);
    }
    private void OnInfectedTroopViewPressed(int index)
    {
        infectedTroops[index].gameObject.SetActive(false);
        GameManager.Instance.KillTroopWithInfection(index);
    }
}

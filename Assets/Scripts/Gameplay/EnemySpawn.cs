using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{ 
    [SerializeField]
    private Transform enemyPrefab;

    private BaseSquadFormation Formation;

    public IEnumerator SpawnEnemies(int enemiesToSpawn)
    {
        List<EnemyCreature> spawnedEnemies = new List<EnemyCreature>();
        int spawningEmissionRate = SettingsService.GameSettings.spawningEmissionRate;
        Formation = new HordeFormation(enemiesToSpawn);
        Formation.CalculatePosition(transform.position);
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Transform trans = Instantiate<Transform>(enemyPrefab, transform.position, transform.rotation, transform);
            spawnedEnemies.Add(new EnemyCreature(trans.gameObject, Formation.GetPositionOfTroop(i, transform.position),
                                                 SettingsService.GameSettings.WeakZombi));
            if(i % spawningEmissionRate == spawningEmissionRate -1)
            {
                yield return 0;
            }
        }
        if(EnemiesManager.Instance != null)
        {
            EnemiesManager.Instance.AddEnemyGroup(new EnemiesGroup(spawnedEnemies, transform.position));
        }
    }
}

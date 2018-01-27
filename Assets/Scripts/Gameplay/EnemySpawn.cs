using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{ 
    [SerializeField]
    private Transform enemyPrefab;

    private BaseSquadFormation Formation;

    private bool isInitialized = false;
    private int enemiesPerWave;
    private float wavesPeriod;

    private float timer = 0;

    public void Init(int _enemiesPerWave, float _wavesPeriod, float _offset)
    {
        isInitialized = true;
        timer = _offset;
        enemiesPerWave = _enemiesPerWave;
        wavesPeriod = _wavesPeriod;
        Formation = new HordeFormation(enemiesPerWave);
        Formation.CalculatePosition(transform.position);
        if(wavesPeriod < 0)
        {
            SpawnEnemies();
        }
    }
	
	private void Update ()
    {
		if(isInitialized)
        {
            timer += Time.deltaTime;
            if(wavesPeriod > 0 && timer > wavesPeriod)
            {
                timer = 0;
                SpawnEnemies();
            }
        }
	}

    private void SpawnEnemies()
    {
        List<EnemyCreature> spawnedEnemies = new List<EnemyCreature>();
        Formation.CalculatePosition(transform.position);
        for (int i = 0; i < enemiesPerWave; i++)
        {
            Transform trans = Instantiate<Transform>(enemyPrefab, transform.position, transform.rotation, transform);
            spawnedEnemies.Add(new EnemyCreature(trans.gameObject, Formation.GetPositionOfTroop(i)));
        }
        if(EnemiesManager.Instance != null)
        {
            EnemiesManager.Instance.AddEnemyGroup(new EnemiesGroup(spawnedEnemies));
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesGroup
{
    private HordeFormation formation;
    private List<EnemyCreature> enemies;

    public EnemiesGroup(List<EnemyCreature> _enemies)
    {
        enemies = _enemies;
        formation = new HordeFormation(enemies.Count);
    }

    public void SetTarget(Vector2 target)
    {
        formation.CalculatePosition(target);
        for(int i = 0; i < enemies.Count; i++)
        {
            enemies[i].UpdatePosition(formation.GetPositionOfTroop(i));
        }
    }
    
    //kiedy żołnierz znajduje się w zasięgu, przeciwnik przestaje podążać do celu grupowego
    //wyznacza własny cel na najbliższego przeciwnika
}

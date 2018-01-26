using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTroop
{
    public GameObject TroopGO;

    public int Index;
    public Vector2 Position;

    public PlayerTroop(GameObject _go, int _index, Vector2 _position)
    {
        TroopGO = _go;
        Index = _index;
        Position = _position;
    }

    public void UpdatePosition(Vector2 target)
    {

    }
}

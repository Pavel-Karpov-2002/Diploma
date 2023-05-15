using System;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
public class TileParameters
{
    [SerializeField] private Tile tile;
    [SerializeField] private float chanceOfReceiving;

    public Tile Tile => tile;
    public float ChanceOfReceiving => chanceOfReceiving;
}

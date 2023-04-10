using System;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
public class MazeParameters
{
    [SerializeField][Min(0)] private int maxRoomRows = 15;
    [SerializeField][Min(0)] private int maxRoomColumns = 15;

    [SerializeField] private int width = 40;
    [SerializeField] private int height = 40;
    [SerializeField] private Tile floorTile;
    [SerializeField] private Tile wallTile;

    public int MaxRoomRows => maxRoomRows;
    public int MaxRoomColumns => maxRoomColumns;
    public int Width => width;
    public int Height => height;
    public Tile FloorTile => floorTile;
    public Tile WallTile => wallTile;
}

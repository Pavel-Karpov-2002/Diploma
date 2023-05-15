using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "MazeParameters", menuName = "CustomParameters/MazeParameters")]
public class MazeParameters : ScriptableObject
{
    [SerializeField][Min(0)] private int maxRoomRows = 15;
    [SerializeField][Min(0)] private int maxRoomColumns = 15;

    [SerializeField] private int width = 40;
    [SerializeField] private int height = 40;
    [SerializeField] private List<TileParameters> floorTile;
    [SerializeField] private List<Tile> wallTile;

    public int MaxRoomRows => maxRoomRows;
    public int MaxRoomColumns => maxRoomColumns;
    public int Width => width;
    public int Height => height;
    public List<TileParameters> FloorTile => floorTile;
    public List<Tile> WallTile => wallTile;

}

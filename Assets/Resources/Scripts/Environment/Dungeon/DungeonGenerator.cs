using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonGenerator : MonoBehaviour
{
    [SerializeField] protected Tilemap wallsMap;
    [SerializeField] protected Tilemap floorMap;
    [SerializeField] private NPCGeneratorScript npcGeneratorScript;
    [SerializeField] private Vector2 gridScale;
    [SerializeField] private MazeParameters mazeParameters;

    [NonSerialized] public static List<Rect> rooms;

    private void Start()
    {
        GenerateDungeon();
    }

    public void GenerateDungeon()
    {
        wallsMap.ClearAllTiles();
        floorMap.ClearAllTiles();
        rooms = new List<Rect>();
        BinarySpacePartitioningAlgorithm.StartGenerate(wallsMap, floorMap, mazeParameters);
        npcGeneratorScript.Generator(mazeParameters, rooms, gridScale);
        PlayerGeneratorScript.SetPlayerPosition(mazeParameters, rooms, gridScale);
    }
}
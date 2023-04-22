using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonGenerator : MonoBehaviour
{
    [SerializeField] protected Tilemap wallsMap;
    [SerializeField] protected Tilemap floorMap;
    [SerializeField] private NPCGeneratorScript npcGeneratorScript;
    [SerializeField] private Vector2 gridScale;
    [SerializeField] private GameParameters parameters;

    [NonSerialized] public static List<Rect> rooms;

    private void Start()
    {
        GenerateDungeon(parameters.Maze);
    }

    public void GenerateDungeon(MazeParameters parameters)
    {
        wallsMap.ClearAllTiles();
        floorMap.ClearAllTiles();
        rooms = new List<Rect>();
        BinarySpacePartitioningAlgorithm.StartGenerate(wallsMap, floorMap, parameters);
        npcGeneratorScript.Generator(parameters, rooms, gridScale);
        PlayerGeneratorScript.SetPlayerPosition(parameters, rooms, gridScale);
    }
}
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

[CustomEditor(typeof(DungeonGenerator))]
public class ButtonDungeonGenerator : Editor
{
    [SerializeField] private GameParameters parameters;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        DungeonGenerator dungeonGenerator = (DungeonGenerator)target;

        if (GUILayout.Button("Generate dungeon"))
        {
            dungeonGenerator.GenerateDungeon(parameters.Maze,new Tilemap(), new Tilemap());
        }
    }
}

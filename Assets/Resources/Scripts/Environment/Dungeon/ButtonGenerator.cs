using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DungeonGenerator))]
public class ButtonGenerator : Editor
{
    [SerializeField] private GameParameters parameters;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        DungeonGenerator dungeonGenerator = (DungeonGenerator)target;

        if (GUILayout.Button("Generate dungeon"))
        {
            dungeonGenerator.GenerateDungeon();
        }
    }
}

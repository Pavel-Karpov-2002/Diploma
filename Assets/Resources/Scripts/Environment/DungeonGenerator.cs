using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonGenerator : MonoBehaviour
{
    [SerializeField] private Tilemap wallsMap;
    [SerializeField] private Tilemap floorMap;
    private List<Rect> rooms; 

    public void GenerateDungeon(MazeParameters parameters, Tilemap floorLayer, Tilemap wallsLayer)
    {
        wallsMap.ClearAllTiles();
        floorMap.ClearAllTiles();
        rooms = new List<Rect>();

        BSP(new Rect(0, 0, parameters.Width, parameters.Height), parameters);

        foreach (Rect room in rooms)
        {
            for (int i = (int)room.x; i < room.x + room.width; i++)
            {
                for (int j = (int)room.y; j < room.y + room.height; j++)
                {
                    Vector3Int pos = new Vector3Int(i, j, 0);
                    if (i == room.x || j == room.y || i == parameters.Width - 1 || j == parameters.Height - 1)
                    {
                        SetTileInTilemap(pos, wallsMap, parameters.WallTile);
                    }
                }
            }
        }

        for (int i = 0; i < rooms.Count - 1; i++)
        {
            ConnectRooms(rooms[i], rooms[i + 1], parameters, wallsMap);
        }

        for (int i = 0; i < parameters.Width; i++)
        {
            for (int j = 0; j < parameters.Height; j++)
            {
                Vector3Int pos = new Vector3Int(i, j, 0);
                SetTileInTilemap(pos, floorMap, parameters.FloorTile);
            }
        }
    }

    private void SetTileInTilemap(Vector3Int position, Tilemap tilemaps, Tile tile)
    {
        tilemaps.SetTile(position, tile);
    }

    private void BSP(Rect rect, MazeParameters parameters)
    {
        if (rect.width < parameters.MaxRoomRows || rect.height < parameters.MaxRoomColumns)
        {
            rooms.Add(rect);
            return;
        }

        bool splitHorizontally = Random.value > 0.5f;

        if (rect.width > rect.height && rect.width / rect.height >= 1.25f)
        {
            splitHorizontally = false;
        }
        else if (rect.height > rect.width && rect.height / rect.width >= 1.25f)
        {
            splitHorizontally = true;
        }

        int max = (splitHorizontally ? (int)rect.height : (int)rect.width) - 10;
        int split = Random.Range(10, max);

        Rect leftRect;
        Rect rightRect;

        if (splitHorizontally)
        {
            leftRect = new Rect(rect.x, rect.y, rect.width, split);
            rightRect = new Rect(rect.x, rect.y + split, rect.width, rect.height - split);
        }
        else
        {
            leftRect = new Rect(rect.x, rect.y, split, rect.height);
            rightRect = new Rect(rect.x + split, rect.y, rect.width - split, rect.height);
        }

        BSP(leftRect, parameters);
        BSP(rightRect, parameters);
    }

    private void ConnectRooms(Rect room1, Rect room2, MazeParameters parameters, Tilemap floorLayer)
    {
        Vector2 center1 = new Vector2(room1.x + room1.width / 2f, room1.y + room1.height / 2f);
        Vector2 center2 = new Vector2(room2.x + room2.width / 2f, room2.y + room2.height / 2f);

        if (floorLayer.GetTile(new Vector3Int((int)center1.x, (int)center1.y)) != null)
        {
            CreateHorizontalTunnel((int)center1.x, (int)center2.x, (int)center1.y, floorLayer, parameters.FloorTile);
            CreateVerticalTunnel((int)center2.y, (int)center1.y, (int)center2.x, floorLayer, parameters.FloorTile);
        }
        else
        {
            CreateVerticalTunnel((int)center1.y, (int)center2.y, (int)center1.x, floorLayer, parameters.FloorTile);
            CreateHorizontalTunnel((int)center2.x, (int)center1.x, (int)center2.y, floorLayer, parameters.FloorTile);
        }
    }

    private void CreateHorizontalTunnel(int x1, int x2, int y, Tilemap layer, Tile tileSprite)
    {
        for (int x = Mathf.Min(x1, x2); x <= Mathf.Max(x1, x2); x++)
        {
            SetPositionTile(new Vector3Int(x, y, 0), layer, tileSprite);
        }
    }

    private void CreateVerticalTunnel(int y1, int y2, int x, Tilemap layer, Tile tileSprite)
    {
        for (int y = Mathf.Min(y1, y2); y <= Mathf.Max(y1, y2); y++)
        {
            SetPositionTile(new Vector3Int(x, y, 0), layer, tileSprite);
        }
    }

    private void SetPositionTile(Vector3Int position, Tilemap layer, Tile tileSprite)
    {
        layer.SetTile(position, null);
    } 

}
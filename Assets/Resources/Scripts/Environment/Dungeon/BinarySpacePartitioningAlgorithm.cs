using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BinarySpacePartitioningAlgorithm : DungeonGenerator
{
    public static void StartGenerate(Tilemap wallsMap, Tilemap floorMap, MazeParameters parameters)
    {
        BSPAlgorithm(wallsMap, floorMap, new Rect(0, 0, parameters.Width, parameters.Height), parameters);

        CreateRooms(wallsMap, parameters);
        FillFloor(floorMap, parameters);
        GenerateEdgesFloor(wallsMap, parameters);
    }

    private static void BSPAlgorithm(Tilemap wallsMap, Tilemap floorMap, Rect rect, MazeParameters parameters)
    {
        if (rect.width <= parameters.MaxRoomColumns || rect.height <= parameters.MaxRoomRows)
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

        int max = (splitHorizontally ? (int)rect.height : (int)rect.width) - parameters.MaxRoomRows;
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

        BSPAlgorithm(wallsMap, floorMap, leftRect, parameters);
        BSPAlgorithm(wallsMap, floorMap, rightRect, parameters);
    }

    private static void CreateRooms(Tilemap wallsMap, MazeParameters parameters)
    {
        for (int room = 0; room < rooms.Count; room++)
        {
            if (rooms[room].height * rooms[room].width > (parameters.MaxRoomRows * parameters.MaxRoomColumns / 2))
            {
                for (int i = (int)rooms[room].x; i < rooms[room].x + rooms[room].width; i++)
                {
                    for (int j = (int)rooms[room].y; j < rooms[room].y + rooms[room].height; j++)
                    {
                        Vector3Int pos = new Vector3Int(i, j, 0);
                        if (i == rooms[room].x || j == rooms[room].y || i == parameters.Width - 1 || j == parameters.Height - 1)
                        {
                            SetTileInTilemap(pos, wallsMap, parameters.WallTile[((j == rooms[room].y && i != rooms[room].x) ? 0 : 1)]);
                        }
                    }
                }
            }
            else
            {
                rooms.Remove(rooms[room]);
                room--;
            }
        }

        for (int i = 0; i < rooms.Count - 1; i++)
        {
            ConnectRooms(rooms[i], rooms[i + 1], parameters, wallsMap);
        }
    }

    private static void ConnectRooms(Rect room1, Rect room2, MazeParameters parameters, Tilemap floorLayer)
    {
        Vector2 center1 = new Vector2(room1.x + room1.width / 2f, room1.y + room1.height / 2f);
        Vector2 center2 = new Vector2(room2.x + room2.width / 2f, room2.y + room2.height / 2f);
        CreateHorizontalTunnel((int)center2.x, (int)center1.x, (int)center2.y, floorLayer, GetRandomTile(parameters.FloorTile));
        CreateVerticalTunnel((int)center1.y, (int)center2.y, (int)center1.x, floorLayer, GetRandomTile(parameters.FloorTile));
    }

    private static void CreateHorizontalTunnel(int x1, int x2, int y, Tilemap layer, Tile tileSprite)
    {
        for (int x = Mathf.Min(x1, x2); x <= Mathf.Max(x1, x2); x++)
        {
            SetPositionTile(new Vector3Int(x, y, 0), layer);
        }
    }

    private static void CreateVerticalTunnel(int y1, int y2, int x, Tilemap layer, Tile tileSprite)
    {
        for (int y = Mathf.Min(y1, y2); y <= Mathf.Max(y1, y2); y++)
        {
            SetPositionTile(new Vector3Int(x, y, 0), layer);
        }
    }

    private static void SetPositionTile(Vector3Int position, Tilemap layer)
    {
        layer.SetTile(position, null);
    }

    private static void FillFloor(Tilemap floorMap, MazeParameters parameters)
    {
        for (int i = 0; i < parameters.Width; i++)
        {
            for (int j = 0; j < parameters.Height; j++)
            {
                Vector3Int pos = new Vector3Int(i, j, 0);
                SetTileInTilemap(pos, floorMap, GetRandomTile(parameters.FloorTile));
            }
        }
    }

    private static void GenerateEdgesFloor(Tilemap wallsMap, MazeParameters parameters)
    {
        Tile edgeTile = parameters.WallTile[1];
        for (int j = 0; j < parameters.Height; j++)
        {
            SetTileInTilemap(new Vector3Int(0, j, 0), wallsMap, edgeTile);
            SetTileInTilemap(new Vector3Int(parameters.Width - 1, j, 0), wallsMap, edgeTile);
        }

        for (int i = 0; i < parameters.Width; i++)
        {
            SetTileInTilemap(new Vector3Int(i, 0, 0), wallsMap, edgeTile);
            SetTileInTilemap(new Vector3Int(i, parameters.Height - 1, 0), wallsMap, edgeTile);
        }
    }

    private static void SetTileInTilemap(Vector3Int position, Tilemap tilemaps, Tile tile)
    {
        tilemaps.SetTile(position, tile);
    }

    private static Tile GetRandomTile(List<TileParameters> tiles)
    {
        foreach (var tile in tiles)
        {
            if (Random.Range(0, 100) <= tile.ChanceOfReceiving)
                return tile.Tile;
        }
        return new Tile();
    }
}

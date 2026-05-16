using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class TilemapVisualizer : MonoBehaviour
{
    [SerializeField]
    private Tilemap floorTilemap, wallTilemap, itemTilemap, ladderTilemap;
    [SerializeField]
    private TileBase floorTile, wallTile, ladderTile, oresTile;


    public void PaintFloorTiles(HashSet<Vector2Int> floorPositions)
    {
        PaintTiles(floorPositions, floorTilemap, floorTile);
    }

    private void PaintTiles(HashSet<Vector2Int> positions, Tilemap tilemap, TileBase tile)
    {
        foreach (Vector2Int position in positions)
        {
            PaintSingleTile(tilemap, tile, position);
        }
        for (int i = 0; i < positions.Count / 300; i++)
        {
            var ladderLocation = positions.ElementAt(Random.RandomRange(0, positions.Count));
            Debug.Log(ladderLocation);
            PaintSingleLadder(ladderLocation, ladderTilemap, ladderTile);
        }
    }

    private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position)
    {
        var tilePosition = tilemap.WorldToCell((Vector3Int)position);
        tilemap.SetTile(tilePosition, tile);
    }

    public void Clear()
    {
        ladderTilemap.ClearAllTiles();
        floorTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
    }

    internal void PaintSingleBasicWall(Vector2Int position)
    {
        PaintSingleTile(wallTilemap, wallTile, position);
    }

    static internal void PaintSingleLadder(Vector2Int position, Tilemap tilemap, TileBase tile)
    {
        var tilePosition = tilemap.WorldToCell((Vector3Int)position);
        tilemap.SetTile(tilePosition, tile);
    }
}

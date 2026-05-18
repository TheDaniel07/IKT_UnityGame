using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class TilemapVisualizer : MonoBehaviour
{
    [SerializeField]
    private Tilemap floorTilemap, wallTilemap, itemTilemap, ladderTilemap, oresTilemap;
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
            var ladderLocation = positions.ElementAt(Random.Range(0, positions.Count));
            PaintSingleLadder(ladderLocation);
        }
        for (int i = 0; i < positions.Count / 70; i++)
        {
            var oreLocation = positions.ElementAt(Random.Range(0, positions.Count));
            PaintOre(oreLocation);
        }
    }

    private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position)
    {
        var tilePosition = tilemap.WorldToCell((Vector3Int)position);
        tilemap.SetTile(tilePosition, tile);
    }

    public void Clear()
    {
        oresTilemap.ClearAllTiles();
        ladderTilemap.ClearAllTiles();
        floorTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
    }

    internal void PaintSingleBasicWall(Vector2Int position)
    {
        PaintSingleTile(wallTilemap, wallTile, position);
    }

    internal void PaintSingleLadder(Vector2Int ladderPosition)
    {
        PaintSingleTile(ladderTilemap, ladderTile, ladderPosition);
    }

    internal void PaintOre(Vector2Int oreLocation)
    {
        PaintSingleTile(oresTilemap, oresTile, oreLocation);
    }
}

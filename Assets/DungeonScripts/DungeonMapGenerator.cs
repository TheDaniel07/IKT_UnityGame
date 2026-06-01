using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;

public class DungeonMapGenerator : AbstractDungeonGenerator
{

    [SerializeField]
    private SimpleRandomWalkData randomWalkParams;
    [SerializeField] private OreGenerator OreGenerator;
    [SerializeField] private EnemyGenerator EnemyGenerator;

    protected override void RunProceduralGeneration()
    {
        HashSet<Vector2Int> floorPositions = StartRandomWalk();
        tilemapVisualizer.Clear();
        OreGenerator.ClearLevel();
        EnemyGenerator.KillAllEnemies();
        tilemapVisualizer.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);
        OreGenerator.GenerateOres(floorPositions);
        EnemyGenerator.SpawnEnemies(floorPositions);
    }

    protected HashSet<Vector2Int> StartRandomWalk()
    {
        var currentPosition = startPosition;
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();

        for (int i = 0; i < randomWalkParams.iterations; i++)
        {
            var path = ProceduralGeneration.SimpleRandomWalk(currentPosition, randomWalkParams.walkLength);
            floorPositions.UnionWith(path);
            if (randomWalkParams.startRandomEachIteration)
            {
                currentPosition = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
            }
        }
        return floorPositions;
    }

}

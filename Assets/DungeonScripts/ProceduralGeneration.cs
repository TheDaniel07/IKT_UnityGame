using System.Collections.Generic;
using UnityEngine;

public class ProceduralGeneration : MonoBehaviour
{
    public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPosition, int walkLenght)
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();

        path.Add(startPosition);
        var prevPosition = startPosition;

        for (int i = 0; i < walkLenght; i++)
        {
            var newPosition = prevPosition + Direction2D.randomDirection();
            path.Add(newPosition);
            prevPosition = newPosition;
        }
        return path;
    }
}

public static class Direction2D
{
    public static List<Vector2Int> directionList = new List<Vector2Int>()
    {
        new Vector2Int(0, 1), //fel
        new Vector2Int(1, 0), //jobb
        new Vector2Int(0, -1), //le
        new Vector2Int(-1, 0), //bal
    };

    public static Vector2Int randomDirection()
    {
        return directionList[Random.Range(0, directionList.Count)];
    }
}
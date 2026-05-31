using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private OreGenerator oregen;
    [SerializeField] GameObject enemy;
    public void SpawnEnemies(HashSet<Vector2Int> positions)
    {
        

        for (int i = 0; i < positions.Count / 300; i++)
        {
            Vector2Int randomPos = positions.ElementAt(UnityEngine.Random.Range(0, positions.Count));
            Vector3 enemyPos = new Vector3(randomPos.x, randomPos.y)+ new Vector3(0.5f, 0.5f);
            Instantiate(enemy, enemyPos, Quaternion.identity, parent);
        }
    }

    public void KillAllEnemies()
    {
        foreach(Transform child in parent)
        {
            Destroy(child.gameObject);
        }
    }
}

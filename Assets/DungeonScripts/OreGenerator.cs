using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class OreGenerator : MonoBehaviour
{
    public GameObject[] Ores;
    public Transform parent;
    public Tilemap tilemap;
    
    public void GenerateOres(HashSet<Vector2Int> positions)
    {
        for (int i = 0; i < positions.Count / 20; i++)
        {
            Vector2Int randomPos = positions.ElementAt(Random.Range(0, positions.Count));
            Vector3 pos = new(randomPos.x, randomPos.y);
            Vector3 offset = new(0.5f, 0.5f);
            Instantiate(Ores[Random.Range(0, Ores.Length)], pos+offset, Quaternion.identity, parent);
        }
    }
    public void ClearLevel()
    {
        foreach (Transform child in parent)
        {
            Destroy(child.gameObject);
        }
    }


}

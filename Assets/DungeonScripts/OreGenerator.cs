using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class OreGenerator : MonoBehaviour
{
    public GameObject[] Ores;
    public Transform parent;
    [SerializeField] private LadderClimb ladderClimb;
    
    public void GenerateOres(HashSet<Vector2Int> positions)
    {
        for (int i = 0; i < positions.Count / 20; i++)
        {
            Vector2Int randomPos = positions.ElementAt(Random.Range(0, positions.Count));
            Vector3 pos = new(randomPos.x, randomPos.y);
            Vector3 offset = new(0.5f, 0.5f);
            /* 0 - Stone
             * 1 - Coal
             * 2 - Copper
             * 3 - Gold
             * 4- Iron
             * 5 - Ruby
             * 6 - Diamond
             * 7 - Uranium
             */

            if (ladderClimb.Level > 99)
            {
                Instantiate(Ores[Random.Range(3, Ores.Length)], pos + offset, Quaternion.identity, parent); // Gold to Uranium
            }
            else if (ladderClimb.Level > 75)
            {
                Instantiate(Ores[Random.Range(2, 6)], pos + offset, Quaternion.identity, parent); // Copper to Ruby
            }
            else if(ladderClimb.Level > 40)
            {
                Instantiate(Ores[Random.Range(1, 5)], pos + offset, Quaternion.identity, parent); // Coal to Iron
            }
            else if(ladderClimb.Level > 20)
            {
                Instantiate(Ores[Random.Range(0, 3)], pos + offset, Quaternion.identity, parent); // Stone to Copper
            }
            else {
                Instantiate(Ores[Random.Range(0, 2)], pos + offset, Quaternion.identity, parent); // Stone to Coal
            }
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

using UnityEngine;

[CreateAssetMenu(fileName = "SimpleRandomWalkParams" ,menuName = "PCG/SimpleRandomWalkData")]
public class SimpleRandomWalkData : ScriptableObject
{
    public int iterations = 10;
    public int walkLength = 10;
    public bool startRandomEachIteration = true;
}

using UnityEngine;

public class LadderClimb : MonoBehaviour, IInteractable
{
    [SerializeField] private AbstractDungeonGenerator dungeonGenerator;
    public void Interact()
    {
        ClimbLadder();
    }

    private void ClimbLadder()
    {
        dungeonGenerator.GenerateDungeon();
    }
}

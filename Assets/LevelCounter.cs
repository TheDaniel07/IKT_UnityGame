using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelCounter : MonoBehaviour
{
    [SerializeField]
    private LadderClimb ladderClimb;
    [SerializeField]
    private TMP_Text counterText;
    void Update()
    {
        counterText.text = ladderClimb.Level.ToString();
    }
}

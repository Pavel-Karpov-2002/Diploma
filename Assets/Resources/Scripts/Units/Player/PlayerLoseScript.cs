using UnityEngine;

public class PlayerLoseScript : MonoBehaviour
{
    [SerializeField] private PlayerScores playerScores;

    public void PlayerLosed()
    {
        FloorInformation.OccupiedFloor = 0;
        SceneChangeScript.GetInstance().LoseScene();
    }
}

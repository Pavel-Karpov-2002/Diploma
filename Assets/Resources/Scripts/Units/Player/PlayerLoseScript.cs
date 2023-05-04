using UnityEngine;

public class PlayerLoseScript : MonoBehaviour
{
    public void PlayerLosed()
    {
        GameSaveParameters.OccupiedFloor = 0;
        SceneChangeScript.GetInstance().LoseScene();
    }
}

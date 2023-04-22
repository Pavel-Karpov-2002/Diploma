using UnityEngine;

public class PlayerLoseScript : MonoBehaviour
{
    public void PlayerLosed()
    {
        FloorInformation.OccupiedFloor = 0;
        SceneChangeScript.GetInstance().LoseScene();
    }
}

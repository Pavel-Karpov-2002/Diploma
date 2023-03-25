using UnityEngine;

[CreateAssetMenu(menuName = "Parameters", fileName = "GamePatameters")]
public class GameParameters : ScriptableObject
{
    [SerializeField] private EnvironmentParameters environmentParameters;

    public EnvironmentParameters EnvironmentParameters { get { return environmentParameters; } }

}

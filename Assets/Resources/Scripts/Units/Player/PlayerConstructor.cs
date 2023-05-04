using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public abstract class PlayerConstructor : Unit
{
    [SerializeField] private GameParameters gameParameters;

    public static PlayerConstructor instance;
    protected GameParameters GameParameters => gameParameters;

    protected PlayerParameters PlayerParameters { get; private set; }

    private void Awake()
    {
        PlayerParameters = gameParameters.Player;
        if (instance == null && (this is PlayerScores))
            instance = this;
    }

    public static PlayerConstructor GetInstance()
    {
        if (instance == null)
            instance = Resources.FindObjectsOfTypeAll<PlayerConstructor>()[0];

        return instance;
    }

    private void OnDestroy()
    {
        instance = null;
    }
}

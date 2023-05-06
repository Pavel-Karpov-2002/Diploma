using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(PlayerAnimation))]
public class PlayerMovement : PlayerConstructor
{
    [SerializeField] private new PlayerAnimation animation;
    [SerializeField] private SpriteRenderer playerSprite;

    private float speed;
    private Rigidbody2D rb;
    private static PlayerMovement instance;

    public float Speed { get => speed; set => speed = value; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = PlayerParameters.PlayerSpeed;
    }

    private void Update()
    {
        PlayerJoystickMovement();
    }

    private void PlayerJoystickMovement()
    {
        if (MovementJoystick.GetInstance() == null)
            return;

        if (MovementJoystick.GetInstance().JoystickDirection.y != 0)
        {
            rb.velocity = new Vector2(MovementJoystick.GetInstance().JoystickDirection.x * speed, MovementJoystick.GetInstance().JoystickDirection.y * speed);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

        animation.ChangeAnimation(MovementJoystick.GetInstance().JoystickDirection.x, MovementJoystick.GetInstance().JoystickDirection.y, playerSprite);
    }

    public static PlayerMovement GetInstance()
    {
        if (instance == null)
            instance = Resources.FindObjectsOfTypeAll<PlayerMovement>()[0];

        return instance;
    }

    private void OnDestroy()
    {
        instance = null;
    }
}
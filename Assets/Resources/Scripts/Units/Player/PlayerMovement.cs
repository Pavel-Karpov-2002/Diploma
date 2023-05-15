using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(PlayerAnimation))]
public class PlayerMovement : CustomSingleton<PlayerMovement>
{
    [SerializeField] private PlayerParameters playerParameters;
    [SerializeField] private new PlayerAnimation animation;
    [SerializeField] private SpriteRenderer playerSprite;

    private float speed;
    private Rigidbody2D rb;

    public float Speed { get => speed; set => speed = value; }

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
        speed = playerParameters.PlayerSpeed;
    }

    private void Update()
    {
        GameData.Data.AmountMoney = 1000;
        PlayerJoystickMovement();
    }

    private void PlayerJoystickMovement()
    {
        if (MovementJoystick.Instance == null)
            return;
        if (MovementJoystick.Instance.JoystickDirection.y != 0)
            rb.velocity = new Vector2(MovementJoystick.Instance.JoystickDirection.x * speed, MovementJoystick.Instance.JoystickDirection.y * speed);        
        else
            rb.velocity = Vector2.zero;
        animation.ChangeAnimation(MovementJoystick.Instance.JoystickDirection.x, MovementJoystick.Instance.JoystickDirection.y, playerSprite);
    }
}
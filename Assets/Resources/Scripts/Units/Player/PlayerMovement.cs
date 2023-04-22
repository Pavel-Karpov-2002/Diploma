using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(PlayerAnimation))]
public class PlayerMovement : PlayerConstructor
{
    [SerializeField] private new PlayerAnimation animation;
    [SerializeField] private SpriteRenderer playerSprite;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
            rb.velocity = new Vector2(MovementJoystick.GetInstance().JoystickDirection.x * PlayerParameters.PlayerSpeed, MovementJoystick.GetInstance().JoystickDirection.y * PlayerParameters.PlayerSpeed);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

        animation.ChangeAnimation(MovementJoystick.GetInstance().JoystickDirection.x, MovementJoystick.GetInstance().JoystickDirection.y, playerSprite);
    }
}
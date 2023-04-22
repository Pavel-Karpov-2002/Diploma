using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public Animation StateAnimation
    {
        get { return (Animation)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }

    public void ChangeAnimation(float posX, float posY, SpriteRenderer playerSprite)
    {
        if (posY == 0 && posX == 0)
        {
            SetAnimation(Animation.Idle);
            return;
        }

        SetAnimation(Animation.Run);
        playerSprite.flipX = posX > 0;
    }

    private void SetAnimation(Animation anim)
    {
        StateAnimation = anim;
    }
}

public enum Animation
{
    Idle,
    Run
}

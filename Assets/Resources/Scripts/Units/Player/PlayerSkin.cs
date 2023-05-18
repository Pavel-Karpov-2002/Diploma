using UnityEngine;

public class PlayerSkin : PlayerConstructor
{
    [SerializeField] private NPCParameters nPCParameters;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Animator animator;

    public static int SkinNum { get; set; }

    private void Start()
    {
        SetSkin();
    }

    public void SetSkin()
    {
        sprite.sprite = nPCParameters.Students[SkinNum].Skin;
        animator.runtimeAnimatorController = nPCParameters.Students[SkinNum].SkinAnimator;
    }
}

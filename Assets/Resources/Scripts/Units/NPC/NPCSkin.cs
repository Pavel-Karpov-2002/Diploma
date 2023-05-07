using System;
using UnityEngine;

[Serializable]
public class NPCSkin
{
    [SerializeField] private Sprite skin;
    [SerializeField] private AnimatorOverrideController skinAnimator;

    public Sprite Skin => skin;
    public AnimatorOverrideController SkinAnimator => skinAnimator;
}

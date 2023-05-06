using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public abstract class PlayerConstructor : Unit
{
    [SerializeField] private PlayerParameters playerParameters;

    protected PlayerParameters PlayerParameters => playerParameters;
}

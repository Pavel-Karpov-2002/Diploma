using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class NPC : Unit
{
    [SerializeField] private GameParameters gameParameters;

    private bool isWaiting;

    public bool IsWaiting { get => isWaiting; set => isWaiting = value; }

    private void Start()
    {
        isWaiting = true;
    }

    private void ChangeClothes()
    {
        // рандомная генерация одежды
    }

    private void ChangeClothesFromAbove()
    {
        // изменить верхнюю одежду
    }

    private void ChangeClothesFromBelow()
    {
        // изменить нижнюю одежду
    }
}

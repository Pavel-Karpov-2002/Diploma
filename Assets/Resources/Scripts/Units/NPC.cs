using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class NPC : Unit
{
    [SerializeField] protected GameParameters gameParameters;

    private bool isExpectation;

    public bool IsExpectation { get => isExpectation; protected set => isExpectation = value; }

    public delegate void UpdateQuestions(string filePath, string url);

    public static event UpdateQuestions OnUpdateQuestions;

    private void Awake()
    {
        isExpectation = true;
    }

    public abstract void SetExpectation(bool expectation);

    protected void NewQuestions(string filePath, string url)
    {
        OnUpdateQuestions?.Invoke(filePath, url);
    }

    private void SetSkin()
    {
        // рандомная генерация модельки
    }
}
